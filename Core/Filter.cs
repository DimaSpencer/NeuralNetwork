using NeuralNetworkLib.Abstractions;
using NeuralNetworkLib.Core;
using NeuralNetworkLib.Extensions;
using NeuralNetworkLib.Maths;
using System.Drawing;

namespace NeuralNetwork.Core.Models
{
    public class Filter
    {
        private readonly IActivationFunction _activationFunction;
        private List<double[,]> _kernels;
        private double[,] _outputMap;

        public Filter(IEnumerable<double[,]> kernels, IActivationFunction activationFunction = null)
        {
            if (kernels is null)
                throw new ArgumentNullException(nameof(kernels), "Filter is null");

            _kernels = kernels.ToList();
            _activationFunction = activationFunction ?? new Sigmoid();
        }

        public IReadOnlyList<double[,]> Kernels => _kernels.AsReadOnly();
        public int KernelSize => _kernels[0].GetLength(0);

        public void InitializeRandomValues()
        {
            foreach (var kernel in _kernels)
                WeightsInitializer.InitializeMatrixKernelRandomValues(kernel);
        }

        public double[,] Process(IEnumerable<double[,]> canals, int stride = 1, int bias = 0)//если будет 2 то будет много ошибок, нужно предусмотреть этот вариант, используя формулу
        {
            if (canals.Count() != _kernels.Count)
                throw new ArgumentOutOfRangeException(nameof(canals), "The number of channels must be equal to the number of kernels.");
            if (stride <= 0)
                throw new ArgumentOutOfRangeException(nameof(stride));


            double[,] firstCanal = canals.ElementAt(0);
            int rowCount = firstCanal.GetLength(0);
            int columnCount = firstCanal.GetLength(1);

            int toRowIndex = rowCount - KernelSize; //размер по x y должен быть всегда одинаковый
            int toColumnIndex = columnCount - KernelSize;

            double[,] outputMap = new double[rowCount - 1, columnCount - 1]; //TODO: тут формулу подсмотреть

            for (int rowIndex = 0; rowIndex <= toRowIndex; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex <= toColumnIndex; columnIndex++)
                {
                    double pixel = 0.0;
                    for (int canalIndex = 0; canalIndex < _kernels.Count; canalIndex++)
                    {
                        double[,] area = canals.ElementAt(canalIndex).GetAreaByIndex(new Point(columnIndex, rowIndex), KernelSize, KernelSize);
                        double[,] kernel = _kernels[canalIndex];

                        pixel += CalculateConvolution(area, kernel);
                    }
                    //pixel = _activationFunction.Calculate(pixel);

                    outputMap[rowIndex, columnIndex] = pixel;
                }
            }

            _outputMap = outputMap;
            return _outputMap;
        }

        private double CalculateConvolution(double[,] input, double[,] kernel)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input is null");

            int rowCount = input.GetLength(0);
            int coulumnCount = input.GetLength(1);

            double result = 0.0;

            for (int columnIndex = 0; columnIndex < coulumnCount; columnIndex++)
            {
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    double value = kernel[columnIndex, rowIndex] * input[columnIndex, rowIndex];
                    result += value;
                }
            }

            return result;
        }
    }
}
