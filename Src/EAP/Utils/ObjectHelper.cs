using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;

namespace EAP.Utils
{
    public class ObjectHelper
    {
        public static object Clone(object obj)
        {
            if (object.ReferenceEquals(obj, null)) return null;
            if (obj is ICloneable)
                return ((ICloneable)obj).Clone();
            return DoBinaryClone(obj);
        }

        public static object BinaryClone(object obj)
        {
            if (object.ReferenceEquals(obj, null)) return null;
            return DoBinaryClone(obj);
        }

        static object DoBinaryClone(object obj)
        {
            using (MemoryStream buffer = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(buffer, obj);
                buffer.Position = 0;
                object temp = formatter.Deserialize(buffer);
                return temp;
            }
        }

        public static byte[] Serialize(object obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        public static object Deserialize(byte[] bytes)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return formatter.Deserialize(stream);
            }
        }
        
        /// <summary>
        /// json序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                ser.WriteObject(ms, t);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// json反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)ser.ReadObject(ms);
            }
        }
    }
}
