
using System.Collections.Generic;

namespace NeuralNetwork.Core.Models
{
    public class LayerOfKernel
    {
        private readonly List<ConvolutionKernel> _kernels;

        public LayerOfKernel(int kernalSize, int kernalCount)
        {
            if (kernalSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(kernalSize), "Kernal size out of range");
            if (kernalSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(kernalCount), "Kernal count out of range");

            _kernels = new List<ConvolutionKernel>(kernalCount);

            for (int i = 0; i < kernalCount; i++)
            {
                ConvolutionKernel kernal = new(new double[kernalSize, kernalSize]);
                kernal.InitializeRandomValues();
                _kernels.Add(kernal);
            }
        }

        public IList<double[,]> ProcessInput(double[,] input)
        {
            List<double[,]> maps = new(_kernels.Count);
            foreach (var kernal in _kernels)
            {
                double[,] map = kernal.Process(input, stride: 1);
                maps.Add(map);
            }

            return maps;
        }
    }
    public class ConvolutionalNeuralNetwork
    {
        private readonly List<LayerOfKernel> _layers;
        
        public ConvolutionalNeuralNetwork(int convolutionalLayerAmount, int kernelSize = 3)
        {
            if (convolutionalLayerAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(convolutionalLayerAmount), "Convolution layer out of range");
            if (kernelSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(kernelSize), "Kernal size value out of range");

            _layers = new List<LayerOfKernel>(convolutionalLayerAmount);

            for (int i = 0; i < convolutionalLayerAmount; i++)
            {
                //для каждого слоя будет сделано три ядра
                LayerOfKernel kernel = new(kernelSize, 3);
                _layers.Add(kernel);
            }
        }

        public virtual void ProcessData(double[,] matrix, int stride)
        {
            if (matrix is null)
                throw new ArgumentNullException(nameof(matrix), "Image is null");

            double[,] filter = { { 0, 0, 0 }, { 1, 1, 1 }, { 0, 0, 0} };
            double[,] filter2 = { { 1, 1, 1 }, { 1, 1, 1 }, { 0, 0, 0} };

            List<double[,]> maps = 
            foreach (var layer in _layers)
            {
                double[,] map = layer.ProcessInput(matrix);
            }
        }
        //private double[,] CalculateMaxPooling(double[,] matrix)
        //{

        //}
    }
}
