using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using ProtocolBuf12.ECViewModel;
namespace ProtocolBuf12
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(ProtoBuf.Serializer.GetProto<A>());
            StreamWriter examcardDToFile = File.CreateText("ECViewmodel.proto");
            examcardDToFile.WriteLine(ProtoBuf.Serializer.GetProto<ExamCardDTO>());
            //Console.WriteLine(ProtoBuf.Serializer.GetProto<ExamCardDTO>());

            examcardDToFile.Flush();
            examcardDToFile.Close();

            //Person person = new Person();
            //person.name = "Alistair";
            //    person.id = 123;
            //FileStream file = File.Create("person.bin");
            //ProtoBuf.Serializer.Serialize<Person>(file, person);

            var model = ProtoBuf.Meta.TypeModel.Create();
            var fields = typeof(Human).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            //int i = 9;

            foreach (var field in fields)
            {
                ProtoBuf.Meta.MetaType type = model.Add(typeof(Human), false).Add(field.Name);
                //type.AddField(i++, field.Name).IsRequired =true;
            }

            StreamWriter textfile = File.CreateText("humanModel.txt");
            //Console.WriteLine(model.GetSchema(typeof(Human)));

            Human person2 = new Human();
            person2.name = "Alistair";
            person2.id = 123;
            
            //StreamWriter textFile = File.CreateText("text.txt");
            ////Serializer.Serialize<Human>(textFile.BaseStream, person2);
            //textFile.WriteLine("sdf");

            //FileStream file2 = File.Create("human.bin");
            //model.Serialize(file2, person2);
            //file2.Close();
            
            var file3 = File.OpenRead("human.bin");
            Human person3 = new Human();
            model.Deserialize(file3, person3, typeof(Human));
            Console.WriteLine(person3.name);
            file3.Close();


            //Try Custom Serialization and try serializing the both the public and also private members
            DummyData d1 = new DummyData("name1", "description1", 23);
            FileStream file4 = File.Create("Dummydata.bin");
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(file4, d1);
            file4.Close();

            FileStream file5 = File.OpenRead("Dummydata.bin");
            DummyData d2 = (DummyData)b.Deserialize(file5);
            Console.WriteLine("Binary Read {0}, {1}", d2.name, d2.GetPrivate());

            //Try XML serialization - Look at how GetObjectData is called for SOAP serialization
            // 1. XML serialization does not need the Serializable attribute. Works without it. 
            // 2. it just blindly serializes the public properties.
            DummyData d3 = new DummyData("name2", "description2", 99);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DummyData));

            StreamWriter xmlStream = File.CreateText("XmlFile.xml");
            xmlSerializer.Serialize(xmlStream, d3);
            xmlStream.Close();

            StreamReader xmlReadStream = File.OpenText("XmlFile.xml");
            DummyData d4 = (DummyData)xmlSerializer.Deserialize(xmlReadStream);
            Console.WriteLine("XML Read -{0} {1}", d4.name, d4.GetPrivate());

            //Try SOAP serialization 
            DummyData d5 = new DummyData("name3", "description3", 110);
            FileStream soapFile = File.Create("XmlFileSoap.xml");
            SoapFormatter soapFormatter = new SoapFormatter();
            soapFormatter.Serialize(soapFile, d5);
            soapFile.Close();

            FileStream soapReadFile = File.OpenRead("XmlFileSoap.xml");
            DummyData d6 = (DummyData)soapFormatter.Deserialize(soapReadFile);
            Console.WriteLine("Soap Read = {0}, {1}, {2}", d6.GetPrivate(), d6.name, d6.description);

            //Try JSON serialization
            DummyData d6Json = new DummyData("nameJSON", "JSON serialization", 120);
            FileStream file = new FileStream("JSONStream.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(file);
            var JsonSerializerInstance = JsonSerializer.Create();
            JsonSerializerInstance.Serialize(sw, d6Json);
            StringBuilder s = new StringBuilder();
            TextWriter tw = new StringWriter(s);
            JsonSerializerInstance.Serialize(tw, d6Json);
            Console.WriteLine(s);
            
            sw.Close();
            file.Close();

        }
    }

    public class Human
    {
        public string name;
        public Int32 id;
    }


    [ProtoContract]
    public class Person
    {
        [ProtoMember(1, IsRequired=true)]
        public string name;
        [ProtoMember(2)]
        public Int32 id;

        //public string Name { get; set; }
        //public Int32 Id { get; set; }
    }

    [ProtoContract(ImplicitFields=ImplicitFields.AllFields)]
    [ProtoInclude(2, typeof(Derived))]
    public class Base
    {
        //[ProtoMember(3)]
        public string stringInBase = "stringInBase";
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class Derived: Base
    {
        //[ProtoMember(4)]
        public string stringInDerived = "stringInDerived";
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class A
    {
        //[ProtoMember(1)]
        //Base b = new Derived();
        public ObservableCollection<Base> listBase = new ObservableCollection<Base>();
        public SampleEnum sampleEnum = SampleEnum.First;
    }


    public enum SampleEnum
    {
        First = 0,
        Second = 1
    }
}
