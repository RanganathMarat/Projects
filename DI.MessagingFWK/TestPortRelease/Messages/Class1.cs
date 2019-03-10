    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using Philips.DI.Interfaces.Services.Messaging.Model;

namespace CreateMDBroker
{
    [Serializable]
    public class MdBrokerRequestMessage : Message
    {
        /// <summary>
        /// MDBrokerInputString
        /// </summary>
        public string MdBrokerInputString { get; set; }
    }

    [Serializable]
    public class MdBrokerResponseMessage : Message
    {
        /// <summary>
        /// 
        /// </summary>
        public string MdBrokerOutputString;
    }


}
