using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ProtoBuf;

namespace SampleInheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the Proto File
            StreamWriter protoDerivedClass = File.CreateText("ProtoDerivedClass.proto");
            Console.WriteLine(ProtoBuf.Serializer.GetProto<DerivedClass>());
            protoDerivedClass.WriteLine(Serializer.GetProto<DerivedClass>());
            protoDerivedClass.Close();


            // Create and instance and serialize deserialize ( Complete Inheritance with reference to interface)
            BaseInterface derived = new DerivedClass();
            DerivedClass dummy = new DerivedClass();
            dummy.Name= "internal";
            derived.Reference = dummy;
            Stream serializeStream = new MemoryStream();
            ProtoBuf.Serializer.Serialize<DerivedClass>(serializeStream, (DerivedClass)derived);
            serializeStream.Seek(0, 0);

            BaseInterface derivedSecond = ProtoBuf.Serializer.Deserialize<DerivedClass>(serializeStream);

            Console.WriteLine(derivedSecond.Reference.Name);

            // Create an instance of SImple Inheritance and then serialize an deseriliaze
            SimpleBaseInterface simpleDerived = new SimpleDerivedClass();
            simpleDerived.Name = "simpleInheritance";

            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize<SimpleDerivedClass>(ms, (SimpleDerivedClass)simpleDerived);
                ms.Seek(0, 0);


                SimpleDerivedClass simpleDerived2 = ProtoBuf.Serializer.Deserialize<SimpleDerivedClass>(ms);    
                Console.WriteLine(simpleDerived2.Name);

            }

        }
    }
}

