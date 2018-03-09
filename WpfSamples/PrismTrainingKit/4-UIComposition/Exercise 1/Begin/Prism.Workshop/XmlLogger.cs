using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Linq;
using Microsoft.Practices.Prism.Logging;
using Workshop.Infrastructure;

namespace Prism.Workshop
{
    public class XmlLogger : ILoggerFacade
    {
        private XDocument document;

        public XmlLogger()
        {
            document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("entries"));
        }

        public void Log(string message, Category category, Priority priority)
        {
            document.Element("entries").Add
                (new XElement
                    ("entry",
                        new XElement("message", message),
                        new XElement("category", category),
                        new XElement("priority", priority)
                    )
                );

            this.SaveLogFile();
        }
        
        private void SaveLogFile()
        {
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isoStream =
                    new IsolatedStorageFileStream(Constants.LogOutputName, FileMode.Create, isoStore))
                {
                    this.document.Save(isoStream);
                }
            }
        }

        public static string ReadLogFile()
        {
            string contents;
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (StreamReader reader =
                        new StreamReader(isoStore.OpenFile(Constants.LogOutputName,
                            FileMode.Open, FileAccess.Read)))
                {
                    contents = reader.ReadToEnd();
                }
            }
            return contents;
        }
    }
}

