using NeuralNetworkLib.Core;

namespace NeuralNetworkLib.Abstractions
{
    public interface INeuralNetwork
    {
        IEnumerable<LayerOfNeurons> LayerOfNeurons { get; }
        IEnumerable<double> ProcessData(IEnumerable<double> inputData);
    }
}
