using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MangaViewer.Foundation.Helper
{
    public static class SerializerHelper
    {
        public static T JsonDeserialize<T>(string str)
        {
            if (string.IsNullOrEmpty(str))
                return default(T);
            return (T)JsonDeserialize(str, typeof(T));
        }

        public static object JsonDeserialize(string str, Type type)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(type);
            using (Stream stream = new MemoryStream(str.GetBytes()))
            {
                return serializer.ReadObject(stream);
            }
        }


        public static string JsonSerializer(object target)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(target.GetType());
            using (Stream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, target);
                return stream.ReadToEnd();
            }
        }

        public static T XMLDeserialize<T>(string str)
        {
            if (string.IsNullOrEmpty(str))
                return default(T);
            return (T)XMLDeserialize(str, typeof(T));
        }


        public static object XMLDeserialize(string str, Type type)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);
            using (StringReader reader = new StringReader(str))
            {
                return serializer.Deserialize((TextReader)reader);
            }
        }



        public static string XmlSerializer(object target)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(target.GetType());
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize((TextWriter)writer, target);
                return writer.ToString();
            }
        }
    }


}
