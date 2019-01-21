using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Sharing.Core
{
    public static class XmlExtension
    {
        public static T DeserializeFromXml<T>(this string document)
          where T : class
        {
            return document.DeserializeFromXml(typeof(T)) as T;
        }
        public static object DeserializeFromXml(this string document, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            using (var stream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(document), false))
            {
                return serializer.Deserialize(stream);
            }
        }
        public static string SerializeToXml(this object obj)
        {
            MemoryStream stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(obj.GetType());
            //序列化对象  
            xml.Serialize(stream, obj);
            stream.Position = 0;
            using (var sr = new StreamReader(stream))
            {
                string text = sr.ReadToEnd();
                stream.Dispose();
                return text;
            }
        }



    }
}
