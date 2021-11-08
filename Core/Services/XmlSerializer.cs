using NeuralNetworkLib.Abstractions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace NeuralNetworkLib.Core
{
    public class XmlSerializer : ISerializer
    {
        public string Serialize<T>(T obj)
        {
            using MemoryStream memoryStream = new();
            DataContractSerializer serializer = new(obj.GetType());

            serializer.WriteObject(memoryStream, obj);

            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        public T Deserialize<T>(string dataString)
        {
            using MemoryStream memoryStream = new(Encoding.UTF8.GetBytes(dataString));
            DataContractSerializer serializer = new(typeof(T));

            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(memoryStream, Encoding.UTF8, new XmlDictionaryReaderQuotas(), null);

            return (T)serializer.ReadObject(reader);
        }
    }
}
