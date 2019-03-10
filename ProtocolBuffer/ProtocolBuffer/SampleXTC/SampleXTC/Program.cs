using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

// Wire protocol: Int32 len, Int32 messageType, protobuf message, Int32 len...
namespace XTC
{
    // Atomic messages flowing through the XTC socket connection
    namespace Message
    {
        /// <summary>
        /// An envelope is an atomic message send through the wires to the remote end.
        /// It contains meta-data for the communication protocol, such as the transmit counter for error detection.
        /// The actual data payload is contained within specialized child classes.
        /// </summary>
        public class Envelope
        {
            /// <summary>
            /// Strictly monotonously increasing counter. 
            /// </summary>
            /// <remarks>Initially zero in the first message that starts the communication session.</remarks>
            public Int64 TransmitCounter;
        }

        // Commands are specialized envelope messages - when a command is received successfully at the remote end,
        // the remote end is responsible for replying, even just to indicate an error.
        namespace Command
        {
            /// <summary>
            /// An envelope for commanding the remote end.
            /// The low-level protocol assumes that we will get 0..n partial replies and one final reply, after which no more messages shall be sent
            /// in response to this command.
            /// </summary>
            public class CommandEnvelope : Envelope
            {
                /// <summary>
                /// Unique command id, C# serialization turns this automatically to a byte array for 
                /// protobuf transmissions.
                /// </summary>
                public Guid CommandId;
            }

            public abstract class Reply : Envelope
            {
                /// <summary>
                /// When sending a reply asynchronously, we need to tie the reply to the original command.
                /// </summary>
                public Guid OriginalCommandId;
            }

            public class PartialReply : Reply
            {
                public Data.Communication.ProgressData Progress;
            }

            public class ClosingReply : Reply
            {
                public Data.Communication.CommandFailure Failure;
            }


            public class InitializeSession : CommandEnvelope
            {
                public Data.Session.SupportedFeature[] SupportedFeatures;
            }

            public class UninitializeSession : CommandEnvelope
            { }
        }

        // An asynchronous notification from a remote end.
        namespace Notification
        {
            // A totally unsuspected event that can be fired while the communication session is still intact.
            namespace SpontaneousEvent { }

            // If a command has forced the remote end to spawn (semi-)regular event notifications,
            // these as carried over in subscription event envelopes.
            // Note that this can allow pooling of multiple subscription events into a single envelope
            // to reduce protocol chatter.
            namespace SubscriptionEvent { }

        }
    }

    // Actual payload data entries carried inside the envelope messages
    namespace Data
    {
        namespace Communication
        {

            /// <summary>
            /// Extend from this to provide specific details
            /// </summary>
            public class CommandFailure
            {
                /// <summary>
                /// Always provide a stringified version for simple clients
                /// </summary>
                public string FailureReason;
            }

            /// <summary>
            /// Extend this to provide details (e.g., progress as a fractional number, elapsed time counter..)
            /// </summary>
            public class ProgressData
            {
            }

        }

        namespace Session
        {
            public class SupportedFeature
            {
                public Guid FeatureId;

                public string DescriptiveFeatureName;
            }
        }
    }
}


namespace SampleXTC
{
    class Program
    {
        // TODO: initialize via reflection from XTC.Communication namespace, now hardcoded
        static readonly Dictionary<Int32, Type> TypeIdTypeMap = new Dictionary<int, Type> { 
            {
                CalculateId(typeof(XTC.Message.Command.InitializeSession).FullName), 
                typeof(XTC.Message.Command.InitializeSession)
            } };

        // We generate a pseudorandom number from the name
        static int CalculateId(string nameId)
        {
            // Name hash can be considered quite random for our purposes. Field names should be descriptive and rather long
            // In the unfortunate case of a clash within a message type, renaming the field slightly should do the trick.
            // We should also comply we good class design principles and keep the classes rather short (<< 16 fields or so)
            var randomizer = new Random(nameId.GetHashCode());
            // Google protobuf limits the allowed range to 1..536870911 (reserved range 19000-19999) 
            var val = randomizer.Next(1, 536870911 - 10000);
            if (val >= 19000)
            {
                // Skip the reserved range 19000-19999
                val += 10000;
            }
            return val;
        }

        static ProtoBuf.Meta.MetaType ReflectType(ProtoBuf.Meta.RuntimeTypeModel model, Type netType)
        {
            var type = model.Add(netType, false);
            var publicFields = netType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var fieldInfo in publicFields)
            {
                type.AddField(CalculateId(fieldInfo.Name), fieldInfo.Name);
            }
            return type;
        }

        static void Stream(ProtoBuf.Meta.RuntimeTypeModel model, System.IO.Stream stream, XTC.Message.Envelope envelopeMessage) 
        {
            using (var envelopeStream = new System.IO.MemoryStream()) {
                model.Serialize(envelopeStream, envelopeMessage);
                var envelopeBytes = envelopeStream.ToArray();
                var lenBytes = BitConverter.GetBytes((Int32)envelopeBytes.Length);
                stream.Write(lenBytes, 0, lenBytes.Length);
                var typeBytes = BitConverter.GetBytes(CalculateId(envelopeMessage.GetType().FullName));
                stream.Write(typeBytes, 0, typeBytes.Length);
                stream.Write(envelopeBytes, 0, envelopeBytes.Length);
            }
        }

        static KeyValuePair<Int32, XTC.Message.Envelope> DeStream(ProtoBuf.Meta.RuntimeTypeModel model, System.IO.Stream stream)
        {
            var lenBytes = new byte[4];
            stream.Read(lenBytes, 0, lenBytes.Length);
            var length = BitConverter.ToInt32(lenBytes, 0);
            var typeBytes = new byte[4];            
            stream.Read(typeBytes, 0, typeBytes.Length);
            var typeId = BitConverter.ToInt32(typeBytes, 0);
            Type type;
            if (!TypeIdTypeMap.TryGetValue(typeId, out type)) {
                // Unknown message, let's ignore this
                type = typeof(XTC.Message.Envelope);
            }
            return new KeyValuePair<int,XTC.Message.Envelope>(typeId, model.Deserialize(stream, null, type, (int)length) as XTC.Message.Envelope); 
        }

        /// <summary>
        /// Sample serialization-deserialization code
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Serialize 
            byte[] sampleWireData;
            var model = ProtoBuf.Meta.TypeModel.Create();
            using (var stream = new System.IO.MemoryStream())
            {                
                // TODO reflect types from XTC namespace, now types are hardcoded
                Type[] types = new Type[] { typeof(XTC.Message.Command.InitializeSession), typeof(XTC.Message.Envelope) };

                foreach (var netType in types)
                {
                    var protoType = ReflectType(model, netType);
                }

                model.CompileInPlace();

                
                var cmd = new XTC.Message.Command.InitializeSession();                
                Stream(model, stream, cmd);
                
                sampleWireData = stream.ToArray();
            }

            // Let's deserialize from the byte array
            using (var stream = new System.IO.MemoryStream(sampleWireData))
            {
                KeyValuePair<Int32, XTC.Message.Envelope> messageIdDataPair = DeStream(model, stream);
                if (object.ReferenceEquals(messageIdDataPair.Value.GetType(), typeof(XTC.Message.Command.InitializeSession))) {
                    System.Console.Out.WriteLine("Hurrah!");
                }
            }            
        }
    }
}
