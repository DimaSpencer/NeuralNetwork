using NeuralNetworkLib.Core;

namespace NeuralNetworkLib.Abstractions
{
    public interface INeuralNetwork
    {
        IReadOnlyCollection<LayerOfNeurons> LayerOfNeurons { get; }
        IEnumerable<double> ProcessData(IEnumerable<double> inputData);
    }
}
