using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace ProtocolBuf12
{
    [Serializable()]
    public class DummyData : ISerializable
    {
        [NonSerialized]
        public string name;

        public string description;

        private int identifier;

        public DummyData( string name, string description, int identifier)
        {
            this.name = name;
            this.description = description;
            this.identifier = identifier;
        }


        private DummyData(SerializationInfo info, StreamingContext context)
        {            
            SerializationHelper.PrepareObjectForDeserialize(info, context, this);
        }

        public DummyData() { }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            SerializationHelper.PerformObjectPreparation(info, context, this);
        }

        public int GetPrivate()
        {
            return identifier; 
        }

    }


    static class SerializationHelper
    {
        public static void PerformObjectPreparation(SerializationInfo info, StreamingContext context, object serializationObject)
        {
            Type t = serializationObject.GetType();
            FieldInfo[] fi = t.GetFields(BindingFlags.Instance |
                   BindingFlags.NonPublic | BindingFlags.Public);
            foreach (FieldInfo m in fi)
            {
                object fieldValue = m.GetValue(serializationObject);
                info.AddValue(m.Name, fieldValue);
            }
           
        }

        public static void PrepareObjectForDeserialize(SerializationInfo info ,StreamingContext context, object deSerializationObject)
        {
            Type t = deSerializationObject.GetType();
            FieldInfo[] fi = t.GetFields(BindingFlags.Instance |
                   BindingFlags.NonPublic | BindingFlags.Public);
            foreach (FieldInfo m in fi)
            {
                m.SetValue(deSerializationObject, info.GetValue(m.Name, m.FieldType));                
            }
        }
    }
}
