namespace NeuralNetworkLib.Abstractions
{
    public interface IDataProvider
    {
        Task SaveAsync(string filePath);
        Task LoadAsync(string filePath);
    }
}
