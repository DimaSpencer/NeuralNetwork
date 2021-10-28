namespace NeuralNetworkLib.Abstractions
{
    public interface ISerializer
    {
        string Serialize<T>(T obj);
        T Deserialize<T>(string dataString);
    }
}
