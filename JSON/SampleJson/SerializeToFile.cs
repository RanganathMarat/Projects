using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SampleJson
{
    static class SerializeToFile
    {
        public static void SerializeToAFile()
        {
            List<CompilerWarningSuppression> suppressionList = new List<CompilerWarningSuppression>();
            suppressionList.Add(new CompilerWarningSuppression
            {
                WarningId = "31000",
                AllowedCount = 2,
                NamespaceWithViolation = "Philips.DI.Services.Logging",
                Rationale = "Linker warning due to CPU Architecutre difference"
            });

            StreamWriter writer = new StreamWriter("Sample.json");
            writer.WriteLine("{0}", JsonConvert.SerializeObject(suppressionList));
            writer.Close();

            Console.WriteLine( "{0}", JsonConvert.SerializeObject(suppressionList));
        }
    }


    class CompilerWarningSuppression
    {
        public string WarningId { get; set; }

        public int AllowedCount { get; set; }

        public string NamespaceWithViolation { get; set; } 

        public string Rationale { get; set; }

    }
}
