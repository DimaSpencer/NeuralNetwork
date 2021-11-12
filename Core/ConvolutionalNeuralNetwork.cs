using NeuralNetwork.Core.Models;
using System.Collections.Generic;

namespace NeuralNetwork.Core
{
    public class ConvolutionalNeuralNetwork
    {
        private readonly List<ConvolutionalLayer> _convolutionalLayer;
        private readonly List<SubsamplingLayer> _subsamplingLayers;
        public ConvolutionalNeuralNetwork(int convolutionalLayerAmount,int inputCanals, int kernelSize = 3)
        {
            if (convolutionalLayerAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(convolutionalLayerAmount), "Convolution layer out of range");
            if (inputCanals <= 0)
                throw new ArgumentOutOfRangeException(nameof(inputCanals));
            if (kernelSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(kernelSize), "Kernal size value out of range");

            _convolutionalLayer = new List<ConvolutionalLayer>(convolutionalLayerAmount);

            double[,] inputImage = new double[32, 32];
            //первый слой
            ConvolutionalLayer layerOfFilters = new(
                kernelSize: 5,
                filtersCount: 6,
                inputCanals: 1);

            _convolutionalLayer.Add(layerOfFilters);
        }

        public void ProcessCanals(double[,] canal, int stride, int padding = 0)
        {
            ProcessCanals(new List<double[,]> { canal }, stride, padding);
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
            foreach (var layer in _convolutionalLayer)
            {
                lastCanals = layer.ProcessCanals(lastCanals, 3);
                //тут можна пройти процесс сжатия если таков слой имеется
            }
        }
    }
}
