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
            using StreamReader reader = new(memoryStream);

            DataContractSerializer serializer = new(typeof(T));
            serializer.WriteObject(memoryStream, obj);
            memoryStream.Position = 0;

            return reader.ReadToEnd();
        }

        public T Deserialize<T>(string dataString)
        {
            using MemoryStream memoryStream = new(Encoding.UTF8.GetBytes(dataString));
            using StreamReader reader = new(memoryStream);

            DataContractSerializer serializer = new(typeof(T));

            return (T)serializer.ReadObject(memoryStream);
        }
    }
}
