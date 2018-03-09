using System;
using System.IO;
using System.Runtime.Serialization;

namespace Philips.PmsMR.Protobuf
{
    /// <summary>
    /// Exception in reading/writing protobuf data types over streams.
    /// </summary>
    /// <remarks>
    /// Used by the serialization class to notify IO exceptions on the stream 
    /// used for serializing and deserializing protobuf data objects
    /// </remarks>
    [Serializable]
    public class StreamIOException: IOException
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public StreamIOException() { }

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="message">Data to be added to protocol failure message</param>
        public StreamIOException(string message) : base(message) {}

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="message">Data to be added to protocol failure message</param>
        /// <param name="innerException"></param>
        public StreamIOException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Serialization ctor.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected StreamIOException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
