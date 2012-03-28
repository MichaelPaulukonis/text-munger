using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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

        // via http://www.joe-stevens.com/2009/12/29/json-serialization-using-the-datacontractjsonserializer-and-c/
        // and
        // http://www.switchonthecode.com/tutorials/csharp-tutorial-xml-serialization
        public static string ToXml_nonworking<T>(this T obj) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var sw = new StringWriter())
            {
                using (var writer = new XmlTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented; // indent the Xml so it's human readable
                    serializer.Serialize(writer, obj);
                    writer.Flush();
                    return sw.ToString();
                }
            }
        }

        public static T FromXMLnonWorking<T>(this T obj, string xml) where T : class
        {
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml)))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(stream) as T;
            }
        }

        public static string ToXml<T>(this T obj) where T : class
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var sw = new Utf8StringWriter())
            {
                using (var writer = new XmlTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented; // indent the Xml so it's human readable
                    serializer.WriteObject(writer, obj);
                    writer.Flush();
                    return sw.ToString();
                }
            }
        }

        public static T FromXML<T>(this T obj, string xml) where T : class
        {
            //using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml)))
            //{
            //    var serializer = new DataContractSerializer(typeof(T));
            //    return serializer.ReadObject(stream) as T;
            //}
            using (var reader = new StringReader(xml))
            {
                using (var xmlReader = XmlReader.Create(reader))
                {
                    var serializer = new DataContractSerializer(typeof(T));
                    //var theObject = (T)serializer.ReadObject(xmlReader);
                    return serializer.ReadObject(xmlReader) as T;
                }
            }
        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }
    }
}