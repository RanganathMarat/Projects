using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace SampleInheritance
{
    [ProtoContract(ImplicitFields=ImplicitFields.AllFields)]
    [ProtoInclude(10, typeof(DerivedClass))]
    interface BaseInterface
    {
        string Name
        {
            get;
            set;
        }

        BaseInterface Reference
        {
            get;
            set;
        }

        bool IAmBase
        {
            get;
        }
        bool identifyOneself();
    }

    [ProtoContract(ImplicitFields=ImplicitFields.AllFields)]
    class DerivedClass : BaseInterface
    {
        private int onlyPrivate = 99;
        private bool iAmDerived = true;
        private string name = null;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private BaseInterface reference;

        public BaseInterface Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        public bool IAmBase
        {
            get
            {
                return !iAmDerived;
            }
        }

        public bool identifyOneself()
        {
            return iAmDerived;
        }
    }

}
