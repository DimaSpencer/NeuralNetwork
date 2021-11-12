using NeuralNetwork.Abstractions;
using NeuralNetworkLib.Core;

namespace NeuralNetwork.Core
{
    public class CNNTopology : ICNNTopology
    {
        public CNNTopology(Dictionary<ConvolutionalLayerMetadata, SubsamplingLayerMetadata> convolutionalLayers, int inputCanalsCount)
        {
            int kernelsCount = inputCanalsCount;
            foreach (var layer in convolutionalLayers.Keys)
            {
                layer.KernelsCount = kernelsCount;
                kernelsCount = layer.FiltersCount;
            }

            ConvolutionalSubsamplingLayerPair = convolutionalLayers;
            ConvolutionalLayers = convolutionalLayers.Keys.ToList();
            SubsamplingLayers = convolutionalLayers.Values.ToList();
        }

        public List<ConvolutionalLayerMetadata> ConvolutionalLayers { get; private set; }
        public List<SubsamplingLayerMetadata> SubsamplingLayers { get; private set; }
        public NeuralNetworkSettings ClassifierTopology { get; private set; }
        public int AllLayersCount => ConvolutionalLayers.Count + SubsamplingLayers.Count;
        public Dictionary<ConvolutionalLayerMetadata, SubsamplingLayerMetadata> ConvolutionalSubsamplingLayerPair { get; private set; }
    }

    public class ConvolutionalLayerMetadata
    {
        public int KernelsCount { get; set; }
        public int FiltersCount { get; set; }
        public int KernelSize { get; set; }
    }

    public class SubsamplingLayerMetadata
    {
        public int KernelSize { get; set; }
    }

}
