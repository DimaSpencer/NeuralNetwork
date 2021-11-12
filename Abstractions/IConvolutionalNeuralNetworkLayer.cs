namespace NeuralNetwork.Abstractions
{
    public interface IConvolutionalNeuralNetworkLayer
    {
        IReadOnlyList<double[,]> Output { get; }
        IList<double[,]> ProcessCanals(IEnumerable<double[,]> canals);
    }
}
