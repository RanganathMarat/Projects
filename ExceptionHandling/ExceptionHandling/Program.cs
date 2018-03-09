using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((object o, UnhandledExceptionEventArgs e) =>
            {
                Console.WriteLine("Exception Caught");
            });
            A();

            for ( int i = 0; i<2500000; i++)
            { Console.WriteLine("...."); }
        }

        static void A() { B(); }

        static void B() { C(); }

        static void C() { D(); }

        static void D() { System.Threading.Thread.Sleep(5000); }
    }

    class B {
        public void Display() { }
    }
}
