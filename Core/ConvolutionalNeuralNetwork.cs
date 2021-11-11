
using System.Collections.Generic;

namespace NeuralNetwork.Core.Models
{
    public class ConvolutionalNeuralNetwork
    {
        private readonly List<LayerOf2DKernel> _layers;
        
        public ConvolutionalNeuralNetwork(int convolutionalLayerAmount, int kernelSize = 3)
        {
            if (convolutionalLayerAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(convolutionalLayerAmount), "Convolution layer out of range");
            if (kernelSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(kernelSize), "Kernal size value out of range");

            _layers = new List<LayerOf2DKernel>(convolutionalLayerAmount);

            for (int i = 0; i < convolutionalLayerAmount; i++)
            {
                //для каждого слоя будет сделано три ядра
                LayerOf2DKernel kernel = new(kernelSize, 3);
                _layers.Add(kernel);
            }
        }

        public void ProcessChanals(double[,] chanal, int stride)
        {
            ProcessChanals(new List<double[,]> { chanal }, stride);
        }

        public void ProcessChanals(IEnumerable<double[,]> chanals, int stride)
        {
            if (chanals is null)
                throw new ArgumentNullException(nameof(chanals), "Image is null");
            if (stride <= 0)
                throw new ArgumentOutOfRangeException(nameof(stride));

            IEnumerable<double[,]> lastChanals = chanals;
            foreach (var layer in _layers)
            {
                lastChanals = layer.ProcessCanals(lastChanals);
                //тут можна пройти процесс сжатия
            }
        }
    }
}
