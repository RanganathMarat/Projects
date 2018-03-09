using ProtoBuf;

namespace Common.Messages
{
    /// <summary>
    /// MessageBase
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic, SkipConstructor = true)]
    [ProtoInclude(1000, typeof(Oporational.Ping))]
    [ProtoInclude(1001, typeof(Oporational.PingResponce))]
    [ProtoInclude(1003, typeof(Oporational.Shutdown))]
    [ProtoInclude(1004, typeof(Oporational.AppClosed))]
    [ProtoInclude(1005, typeof(Oporational.AppStarted))]
    [ProtoInclude(1006, typeof(Execution.DataPoint))]
    [ProtoInclude(1007, typeof(Execution.Ended))]
    [ProtoInclude(1008, typeof(Execution.LoadMethod))]
    [ProtoInclude(1009, typeof(Execution.LoadResult))]
    [ProtoInclude(1010, typeof(Execution.StartExecution))]
    [ProtoInclude(1011, typeof(Execution.Started))]
    [ProtoInclude(1012, typeof(Execution.StopExecution))]
    public class MessageBase
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string TypeName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBase"/> class.
        /// </summary>
        public MessageBase()
        {
            TypeName = GetType().ToString();
        }
    }

    /// <summary>
    /// Oporational
    /// </summary>
    public static class Oporational
    {
        /// <summary>
        ///     AppType
        /// </summary>
        public enum AppType
        {
            /// <summary>
            ///     The manage methods
            /// </summary>
            Methods,

            /// <summary>
            ///     The execute methods
            /// </summary>
            Execute,

            /// <summary>
            ///     The analize results
            /// </summary>
            Results,
            /// <summary>
            /// The equ engine
            /// </summary>
            EquEngine,
            /// <summary>
            /// The data processer
            /// </summary>
            DataProcessor
        }

        /// <summary>
        /// AppStarted
        /// </summary>
        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public class AppStarted : MessageBase
        {
            /// <summary>
            /// The application
            /// </summary>
            public AppType App;
        }

        /// <summary>
        /// AppClosed
        /// </summary>
        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public class AppClosed : MessageBase
        {
            /// <summary>
            /// The application
            /// </summary>
            public AppType App;
        }

        /// <summary>
        /// Shutdown
        /// </summary>
        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public class Shutdown : MessageBase { }

        /// <summary>
        /// Ping
        /// </summary>
        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public class Ping : MessageBase
        {
            /// <summary>
            /// The application name
            /// </summary>
            public AppType App;
        }

        /// <summary>
        /// PingResponce
        /// </summary>
        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public class PingResponce : MessageBase
        {
            /// <summary>
            /// The application name
            /// </summary>
            public AppType App;
        }
    }

    /// <summary>
    /// Execution related
    /// </summary>
    public static class Execution
    {
        /// <summary>
        /// LoadMethod
        /// </summary>
        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public class LoadMethod : MessageBase
        {
            /// <summary>
            /// The method name
            /// </summary>
            public string MethodName;
            /// <summary>
            /// The full name
            /// </summary>
            public string FullName;

        }

        /// <summary>
        /// StartExecution
        /// </summary>
        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public class StartExecution : MessageBase
        {
            /// <summary>
            /// The method name
            /// </summary>
            public string MethodName;
        }

        /// <summary>
        /// Stop Execution
        /// </summary>
        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public class StopExecution : MessageBase
        {
            /// <summary>
            /// The method name
            /// </summary>
            public string MethodName;
        }

        /// <summary>
        /// LoadResult
        /// </summary>
        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public class LoadResult : MessageBase
        {
            /// <summary>
            /// The method name
            /// </summary>
            public string MethodName;
            /// <summary>
            /// The result
            /// </summary>
            public string Result;
            /// <summary>
            /// The error
            /// </summary>
            public bool Error;
        }

        /// <summary>
        /// Start
        /// </summary>
        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public class Started : MessageBase
        {
            /// <summary>
            /// The method name
            /// </summary>
            public string MethodName;
        }

        /// <summary>
        /// End
        /// </summary>
        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public class Ended : MessageBase
        {
            /// <summary>
            /// The method name
            /// </summary>
            public string MethodName;

            /// <summary>
            /// The error
            /// </summary>
            public bool Error;

            /// <summary>
            /// The message
            /// </summary>
            public string Message;
        }

        /// <summary>
        /// DataPoint
        /// </summary>
        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public class DataPoint : MessageBase
        {
            /// <summary>
            /// The graph identifier
            /// </summary>
            public int SignalId;
            /// <summary>
            /// The method name
            /// </summary>
            public string MethodName;
            /// <summary>
            /// The color
            /// </summary>
            public Global.Instructions.Colors Color;
            /// <summary>
            /// The value
            /// </summary>
            public float Value;
            /// <summary>
            /// The time stamp
            /// </summary>
            public float TimeStamp;

            /// <summary>
            /// Returns a <see cref="System.String" /> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String" /> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                return SignalId + "|" + Color + "|" + TimeStamp + "|" + Value;
            }
        }
    }
}
