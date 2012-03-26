using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace TextSourcers
{
    public static class ExtensionMethods
    {
        // via http://www.joe-stevens.com/2009/12/29/json-serialization-using-the-datacontractjsonserializer-and-c/
        public static string ToJSON<T>(this T obj) where T : class
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                return Encoding.Default.GetString(stream.ToArray());
            }
        }

        public static T FromJSON<T>(this T obj, string json) where T : class
        {
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return serializer.ReadObject(stream) as T;
            }
        }
    }
}