﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCharp
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //TestDelegates.TestExecute();
            //TestNullProperty testNullproperty = new TestNullProperty();
            //testNullproperty.Name = null;
            //TryTraceLog.LogMessages();
            //TryTraceLog.TraceWithTraceSource();
            //TryLinq.Func();
            //string[] list = new string[] {"as", "fg", "fhg" };
            //TryWindow.Start();
            //Console.WriteLine(TryMisc.TryDateParse());
            TryUnity.StartTryUnity();
            //Console.WriteLine(TestConstants.Text1);
            //Console.WriteLine(TestConstants.Text2);
        }

    }

    //internal static class TestConstants
    //{
    //    public const string Text1 = "bla";
    //    public const string Text2 = "blas";
    //}
}
