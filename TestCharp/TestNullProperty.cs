using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCharp
{
    class TestNullProperty
    {
        private List<string> name;

        public List<string> Name
        {
            get { return name; }
            set { if (value == null)
                {
                    Console.WriteLine("Value is NULL");
                }
                name = value; 
            }
        }

    }
}
