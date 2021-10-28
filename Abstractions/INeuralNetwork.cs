using NeuralNetworkLib.Core;
using NeuralNetworkLib.Enums;

namespace NeuralNetworkLib.Abstractions
{
    public interface INeuralNetwork
    {
        IReadOnlyCollection<LayerOfNeurons> LayerOfNeurons { get; }
        IEnumerable<double> ProcessData(IEnumerable<double> inputData);
    }
}
