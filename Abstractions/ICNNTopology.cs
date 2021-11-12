using NeuralNetwork.Core;
using NeuralNetworkLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Abstractions
{
    public interface ICNNTopology
    {
        Dictionary<ConvolutionalLayerMetadata, SubsamplingLayerMetadata> ConvolutionalSubsamplingLayerPair { get; }
        List<ConvolutionalLayerMetadata> ConvolutionalLayers { get; }
        List<SubsamplingLayerMetadata> SubsamplingLayers { get; }
        NeuralNetworkSettings ClassifierTopology { get; }
        int AllLayersCount { get; }
    }
}
