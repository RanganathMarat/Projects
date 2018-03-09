using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace SampleInheritance
{
        [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
        [ProtoInclude(10, typeof(SimpleDerivedClass))]
        interface SimpleBaseInterface
        {
            string Name
            {
                get;
                set;
            }

            string identifyOneself();
        }

        [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
        class SimpleDerivedClass : SimpleBaseInterface
        {
            private string name = null;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public string identifyOneself()
            {
                return name;
            }
        }

    [ProtoContract]
    class PlainClass
    {
        private string name;
        [ProtoMember(1)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

}
