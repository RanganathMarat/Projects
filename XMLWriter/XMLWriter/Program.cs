using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace XMLWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("atlantis.txt");

            string pattern = "(.*)=(.*)[;,=](.)";
            string swpart = "SWPART";
            foreach (string line in lines)
            {

                //Console.WriteLine("Procesing the line - {0}", line);
                if (System.Text.RegularExpressions.Regex.IsMatch(line, swpart))
                {
                    MatchCollection m = Regex.Matches(line, pattern);
                    //Console.WriteLine("Procesing the line - {0}", line);
                    foreach (Match m1 in m)
                    {
                        //Console.WriteLine("matching is ---" + m1.Groups[1].Value + "***" + m1.Groups[2].Value);

                        Console.WriteLine("\t\t<RP wstype=\"fixed\">\n" +
            "\t\t\t\t\t\t<NAME>{0}</NAME>\n" +
            "\t\t\t\t\t\t<PATH></PATH>\n" +
            "\t\t\t\t\t\t<VERSION useinws=\"0\">{1}</VERSION>\n" +
            "\t\t</RP>", m1.Groups[1].Value, m1.Groups[2].Value);



                    }
                }

            }

        }
    }
}
