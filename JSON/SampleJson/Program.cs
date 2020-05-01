using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SampleJson
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonSerializer serializer = JsonSerializer.Create();
            NewStudyConstructionLog newStudyConstructionLog = new NewStudyConstructionLog()
                {Status = StatusEnum.Success.ToString(), StudyId = Guid.NewGuid().ToString()};

            string logString = typeof(NewStudyConstructionLog).ToString() + ":" + 
                               JsonConvert.SerializeObject(newStudyConstructionLog);
            Console.WriteLine(logString);
            LogStructuredMessage(newStudyConstructionLog);
        }

        static void LogStructuredMessage(object structuredLogObject)
        {
            string logString = structuredLogObject.GetType().ToString() + ":" +
                               JsonConvert.SerializeObject(structuredLogObject);
            Console.WriteLine(logString);
        }
    }

    class NewStudyConstructionLog
    {
        public string StudyId;
        public string Status;
    }

    class WorkflowStateTransitionsLog
    {
        public string StudyId;
        public string FromState;
        public string ToState;
    }

    class SessionStateIndicationLog
    {
        public string StudyId;
        public string Status;
    }

    class ToolUsage
    {

    }

    enum StatusEnum
    {
        Success,
        Failure
    }

    enum OpenCloseActionEnum
    {
        Open, 
        Close
    }
}
