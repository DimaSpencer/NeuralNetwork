using NeuralNetwork.Abstractions;
using NeuralNetwork.Core.Models;
using NeuralNetworkLib.Abstractions;
using NeuralNetworkLib.Core;

namespace NeuralNetwork.Core
{
    //#region CreateLeNet5
    //double[,] inputImage = new double[32, 32];

    //ConvolutionalLayer convolutionalLayer1 = new(
    //    kernelSize: 5,
    //    filtersCount: 6,
    //    inputCanals: 1);
    //IList<double[,]> map = convolutionalLayer1.ProcessCanals(new List<double[,]> { inputImage });

    //SubsamplingLayer subsamplingLayer1 = new(2);
    //IList<double[,]> map1 = subsamplingLayer1.ProcessCanals(map);

    //ConvolutionalLayer convolutionalLayer2 = new(
    //    kernelSize: 5,
    //    filtersCount: 16,
    //    inputCanals: 1);
    //IList<double[,]> map2 = convolutionalLayer2.ProcessCanals(map1);

    //SubsamplingLayer subsamplingLayer2 = new(2);
    //IList<double[,]> map3 = subsamplingLayer2.ProcessCanals(map1);
    //#endregion

    public class ConvolutionalNeuralNetwork
    {
        private readonly ICNNTopology _topology;
        private readonly INeuralNetwork _neuralNetwork;
        private readonly List<IConvolutionalNeuralNetworkLayer> _layers;

        public ConvolutionalNeuralNetwork(ICNNTopology topology)
        {
            if (topology is null)
                throw new ArgumentNullException(nameof(topology));

            _topology = topology;
            _layers = new List<IConvolutionalNeuralNetworkLayer>(_topology.AllLayersCount);
            _neuralNetwork = new NeuralNetworkBase(topology.ClassifierTopology);

            InitializeLayers();
        }

        public void ProcessCanals(IEnumerable<double[,]> canals, int stride, int padding = 0)
        {
            if (canals is null)
                throw new ArgumentNullException(nameof(canals), "Image is null");
            if (stride <= 0)
                throw new ArgumentOutOfRangeException(nameof(stride));
            if (padding < 0)
                throw new ArgumentOutOfRangeException(nameof(padding));

            IEnumerable<double[,]> lastCanals = canals;
            foreach (var layer in _layers)
            {
                lastCanals = layer.ProcessCanals(lastCanals);
            }

            //TODO: подать на вход класификатору
        }

        private void InitializeLayers()
        {
            foreach (var layerPair in _topology.ConvolutionalSubsamplingLayerPair)
            {
                var convolutionalLayer = layerPair.Key;
                var subsamplingLayer = layerPair.Value;

                _layers.Add(new ConvolutionalLayer(convolutionalLayer.KernelSize, convolutionalLayer.FiltersCount, convolutionalLayer.KernelsCount));

                if(subsamplingLayer != null)
                    _layers.Add(new SubsamplingLayer(subsamplingLayer.KernelSize));
            }
        }
    }
}
