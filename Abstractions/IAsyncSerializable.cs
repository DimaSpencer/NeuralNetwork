namespace NeuralNetworkLib.Abstractions
{
    public interface IAsyncSerializable
    {
        Task SaveAsync(string targetPath, ISerializer serializer);
    }
}