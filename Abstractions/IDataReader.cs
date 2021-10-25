namespace NeuralNetwork.Core
{
    public interface IDataReader
    {
        IList<TRow> Read<TRow>(string source);
    }
}
