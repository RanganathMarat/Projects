using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValueRef
{

    class Dummy
    {
        public int Value { get; set; }
    }

    class Program
    {
        private Dummy myD;

        static void Main(string[] args)
        {
            Dummy d = new Dummy();
            d.Value = 7;
            Program p = new Program();
            p.Change(d);
            p.AnotherChange();
            System.Console.WriteLine(d.Value);
        }

        public void Change(Dummy d)
        {
            myD = d;
            myD.Value = 18;
        }

        public void AnotherChange()
        {
            myD.Value = 99;
        }
    }
}
