using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using System.IO;

namespace TryForNereids
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream file = File.Create("dummy.bin");
            QueryVersionResponseMessage input = new QueryVersionResponseMessage();
            ProtoBuf.Serializer.Serialize<QueryVersionResponseMessage>(file, input);
            file.Close();

            FileStream readFile = File.OpenRead("dummy.bin");
            var read = ProtoBuf.Serializer.Deserialize<QueryVersionResponseMessage>(readFile);
            Console.ReadLine();

        }
    }

    [ProtoContract]
    public class QueryVersionResponseMessage
    {
        /// <summary>
        /// Unique identifier for the original request.
        /// </summary>
        [ProtoMember(4)]
        public Guid RequestToken
        {
            get
            {
                //requestToken = new Guid("ramdom");
                return requestToken;
            }
        }

        /// <summary>
        /// Product name of the software, e.g., R5.1.7L2
        /// </summary>
        [ProtoMember(3)]
        public string Version
        {
            get
            {
                if (value == 66)
                {
                    version = "5.2.0.0";
                }
                return version;
            }
        }

        /// <summary>
        /// MR scanner system type, e.g., WA15 or T30.
        /// </summary>
        [ProtoMember(2)]
        public string SystemType
        {
            get
            {
                
                systemType = "WA15";
                return systemType;
            }
        }

        /// <summary>
        /// MR scanner product model, e.g., "Achieva dStream" or "Ingenia".
        /// </summary>
        [ProtoMember(1)]
        public string ProductModel
        {
            get
            {
                productModel = "Ingenia";
                return productModel;
            }
            set
            {
                productModel = value;
            }
        }

        
        private Guid requestToken;
        
        private string version;
        
        private string systemType;
        
        private string productModel;

        public int value;
    }

}
