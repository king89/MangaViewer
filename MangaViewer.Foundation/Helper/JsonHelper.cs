using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MangaViewer.Foundation.Helper
{
    public class JsonHelper
    {
        /// <summary>
        /// Serialize object to JSON string
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="obj">object instance</param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                serializer.WriteObject(ms, obj);
                ms.Position = 0;

                using (StreamReader reader = new StreamReader(ms))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Deserialize JSON string to a given type of object
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="json">JSON string</param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);
                ms.Dispose();

                return obj;
            }
        }

        /// <summary>
        /// Remove headers from JSON string and deserialize it to a given type of object
        /// </summary>
        /// <typeparam name="T">objec type</typeparam>
        /// <param name="json">JSON string</param>
        /// <param name="headers">header array, e.g. {"Body","checkDevice",...}</param>
        /// <returns></returns>
        public static T Deserialize<T>(string json, string[] headers)
        {
            string body = GetBody(json, headers);

            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(body)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);
                ms.Dispose();

                return obj;
            }
        }

        /// <summary>
        /// Remove headers from JSON string and deserialize it to a given type of object
        /// </summary>
        /// <typeparam name="T">objec type</typeparam>
        /// <param name="json">JSON string</param>
        /// <param name="headers">header array, e.g. {"Body","checkDevice",...}</param>
        /// <returns></returns>
        public static object Deserialize(object obj, string json, string[] headers)
        {
            string body = GetBody(json, headers);
            object responseReturn = new object();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(body)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                responseReturn = serializer.ReadObject(ms);
                ms.Dispose();

                return responseReturn;
            }
        }


        /// <summary>
        /// Remove headers from JSON string
        /// </summary>
        /// <param name="jsonStr">JSON string</param>
        /// <param name="headers">header array, e.g. {"Body","checkDevice",...}</param>
        /// <returns></returns>
        public static string GetBody(string jsonStr, string[] headers)
        {
            Match match;

            string body = jsonStr;

            foreach (string header in headers)
            {
                // (?<=\s*{\s*"Body"\s*:).*(?=\}\s*\z)
                match = Regex.Match(body, string.Format("(?<=\\s*{{\\s*\"{0}\"\\s*:).*(?=\\}}\\s*\\z)", header));

                if (match.Groups.Count == 1)
                {
                    body = match.Groups[0].ToString();
                }
                else
                {
                    throw new Exception("JSON string format is not correct.");
                }
            }

            return body;
        }
    }
}
