#region  Copyright 2014 Koninklijke Philips N.V.
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.IO;
using Philips.PmsMR.Platform.Logging;
using ProtoBuf.Meta;

namespace Philips.PmsMR.Protobuf.Serialization
{
    /// <summary>
    /// Serialization of Google Protobuf data with .NET streams.
    /// </summary>
    public class Serializer : ISerializer
    {
        /// <summary>
        /// Initializing ctor
        /// </summary>
        /// <param name="modeler"></param>
        public Serializer(ModelReflection.Modeler modeler) : this(
            modeler, 
            modeler.TypeMapper.Result, 
            modeler.TypeMapper.Factory, 
            modeler.RuntimeTypeModel ) { }

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="modeler"></param>
        /// <param name="mappingResult"></param>
        /// <param name="typeFactory"></param>
        /// <param name="protobufModel"></param>
        internal Serializer(
            ModelReflection.Modeler modeler,
            ModelReflection.IMappingResult mappingResult, 
            ModelReflection.Factory typeFactory,
            ProtoBuf.Meta.RuntimeTypeModel protobufModel) {
            this.modeler = modeler;
            this.mappingResult = mappingResult;
            this.protobufModel = protobufModel;
            TypeFactory = typeFactory;

            if (!BitConverter.IsLittleEndian)
            {
                throw new ApplicationException("Serializer has been implemented on a little-endian machines only");
            }
        }

        /// <summary>
        /// Converts a datamodel object into bytes and stores them in the provided stream.
        /// </summary>
        /// <param name="dataModelObject"></param>
        /// <param name="stream"></param>
        public void Serialize(object dataModelObject, System.IO.Stream stream) {
            if (dataModelObject == null)
            {
                throw new ArgumentNullException("dataModelObject");
            }

            using (var tmpStream = new MemoryStream())
            {
                protobufModel.Serialize(tmpStream, dataModelObject);
                var objectAsArray = tmpStream.ToArray();

                var idInteger = mappingResult.TypeToIntMap[dataModelObject.GetType()];

                // Wire-protocol:
                //   - bytes 0..3: little endian message length (length+type+object fields) in bytes as Int32
                //                 Note: keepalive message has only the length field i.e. value of 4
                //   - bytes 4..7: little endian message type id as Int32
                //   - bytes 8..N: object bytes of the message
                try
                {
                    stream.Write(BitConverter.GetBytes(objectAsArray.Length + (2 * sizeof(Int32))), 0, sizeof(Int32));
                    stream.Write(BitConverter.GetBytes(idInteger), 0, sizeof(Int32));
                    stream.Write(objectAsArray, 0, objectAsArray.Length);
                    stream.Flush();
                }
                catch (ObjectDisposedException)
                {
                    // Connection has been closed by us, ignore this spurious message in a brutal connection closure.
                    logging.Warning("Aborted serialization, disposed object encountered");
                }
                catch (OperationCanceledException ex)
                {
                    throw new StreamIOException("Operation cancelled", ex);
                }
            }
        }

        /// <summary>
        /// Deserializes a datamodel object from the given stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public object Deserialize(System.IO.Stream stream)
        {
            bool dummy;
            return Deserialize(stream, out dummy);
        }

        /// <summary>
        /// Deserializes a datamodel object from the given stream, and detects zero-length null objects as keepalives.
        /// </summary>
        /// <remarks>
        /// Deserialization with keepalive support is useful for communication channels that need periodic keepalives
        /// with minimum communication overhead.
        /// </remarks>
        /// <param name="stream"></param>
        /// <param name="keepaliveDetected">If the stream contains an object with zero id and length, keepalive flag is set</param>
        /// <returns>null if the stream did not contain an object (e.</returns>
        public object Deserialize(System.IO.Stream stream, out bool keepaliveDetected)
        {
            keepaliveDetected = false;

            if (stream == null)
            {
                // Disposing
                return null;
            }

            try
            {
                // Wire-protocol:
                //   - bytes 0..3: little endian message length (length+type+object fields) in bytes as Int32
                //                 Note: keepalive message has only the length field i.e. value of 4
                //   - bytes 4..7: little endian message type id as Int32
                //   - bytes 8..N: object bytes of the message

                var buffer = new byte[sizeof(Int32)];

                // Read message length field
                var amountRead = ReadAllFromStream(stream, buffer, sizeof(Int32));
                if (amountRead == 0)
                {
                    // Read failed, stream end reached or socket closed
                    return null;
                }
                var messageLengthInBytes = BitConverter.ToInt32(buffer, 0);

                // Check if the message is the keepalive message (the length field is the only field)
                if (messageLengthInBytes == BitConverter.ToInt32(KeepaliveMessageBytes, 0))
                {
                    // A keepalive detected
                    keepaliveDetected = true;
                    return null;
                }

                // Check that message length is at least two Int32 size (length and type fields)
                if (messageLengthInBytes < (2*sizeof (Int32)))
                {
                    throw new SerializationException(
                        String.Format(System.Globalization.CultureInfo.InvariantCulture, 
                            "Message length too short: {0} bytes", messageLengthInBytes), null);
                }

                // Read message type ID
                amountRead = ReadAllFromStream(stream, buffer, sizeof(Int32));
                if (amountRead == 0) {
                    // Read failed, stream end reached or socket closed
                    return null;
                }
                var messageTypeId = BitConverter.ToInt32(buffer, 0);

                Type messageNetType;
                if (!mappingResult.IdToTypeMap.TryGetValue(messageTypeId, out messageNetType))
                {
                    throw new UnknownTypeException(messageTypeId);
                }

                var objectBytes = messageLengthInBytes - (2 * sizeof(Int32));
                var obj = protobufModel.Deserialize(stream, null, messageNetType, objectBytes);
                return obj;
            }
            catch (ObjectDisposedException)
            {
                keepaliveDetected = false;
                logging.Warning("Aborted deserialization, disposed object encountered");
                return null;
            }
        }

        /// <summary>
        /// Factory for creating serializable data instances.
        /// </summary>
        public ModelReflection.Factory TypeFactory { get; private set; }

        /// <summary>
        /// Data content for a keepalive message.
        /// Note: keepalive message has only the length field i.e. value of 4
        /// </summary>
        public static readonly byte[] KeepaliveMessageBytes = BitConverter.GetBytes((Int32)(4));

        internal RuntimeTypeModel ProtobufModel
        {
            get { return protobufModel; }
        }

        private int ReadAllFromStream(Stream stream, byte[] buffer, int count) {
            int numBytesToRead = count;
            int numBytesRead = 0;

            // Read may return less bytes than requested, so be prepared to loop multiple times
            do {
                int n = stream.Read(buffer, numBytesRead, numBytesToRead);
                if (n == 0) {
                    // End of stream reached
                    return 0;
                }
                numBytesRead += n;
                numBytesToRead -= n;
            } while (numBytesToRead > 0);

            return numBytesRead;
        }

        private readonly ProtoBuf.Meta.RuntimeTypeModel protobufModel;

        private readonly ModelReflection.Modeler modeler;
        private readonly ModelReflection.IMappingResult mappingResult;

        private static readonly TraceMessage trace = new TraceMessage("Protobuf", "Serializer");
        private static readonly SystemMessage logging = new SystemMessage("Protobuf", "Serializer");
    }
}
