using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
namespace CallPerl
{
    class Program
    {
        static void Main(string[] args)
        {
            string commandArguments = "/c perl temp.pl";
            System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", commandArguments);
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            processInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Process process = new Process();

            process.StartInfo = processInfo;
            process.Start();
            process.WaitForExit(15000);
            process.Close();
        }
    }
}
