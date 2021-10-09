namespace NeuralNetwork.Core
{
    public interface INeuralNetwork
    {
        IEnumerable<double> ProcessData(IEnumerable<double> inputData)
    }
}
