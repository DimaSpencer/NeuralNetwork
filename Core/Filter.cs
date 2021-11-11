using NeuralNetworkLib.Abstractions;
using NeuralNetworkLib.Core;
using NeuralNetworkLib.Maths;
using System.Drawing;

namespace NeuralNetwork.Core.Models
{
    public class Filter
    {
        private readonly IActivationFunction _activationFunction;
        private List<double[,]> _kernels;
        private double[,] _output;

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

        public double[,] Process(IEnumerable<double[,]> canals, int stride = 1)//если будет 2 то будет много ошибок, нужно предусмотреть этот вариант, используя формулу
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

            double[,] resultCanal = new double[rowCount - 1, columnCount - 1]; //TODO: тут формулу подсмотреть

            for (int rowIndex = 0; rowIndex < toRowIndex; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < toColumnIndex; columnIndex++)
                {
                    double pixel = 0.0;
                    for (int kernelIndex = 0; kernelIndex < _kernels.Count; kernelIndex++)
                    {
                        double[,] area = TakeAreaByIndex(canals.ElementAt(kernelIndex), new Point(columnIndex, rowIndex), KernelSize, KernelSize);
                        double[,] kernel = _kernels[kernelIndex];

                        pixel += CalculateConvolution(area, kernel);
                    }
                    //pixel = _activationFunction.Calculate(pixel);

                    resultCanal[rowIndex, columnIndex] = pixel;
                }
            }

            _output = resultCanal;
            return _output;
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

        private static double[,] TakeAreaByIndex(double[,] source, Point index3D, int width, int height)
        {
            double[,] matrix = new double[height, width];

            int startPointRow = index3D.Y;
            int endPointRow = index3D.Y + height;

            int startPointColumn = index3D.X;
            int endPointColumn = index3D.X + width;

            for (int rowIndex = startPointRow, matrixRow = 0; rowIndex < endPointRow; rowIndex++, matrixRow++)
            {
                for (int columnIndex = startPointColumn, matrixColumn = 0; columnIndex < endPointColumn; columnIndex++, matrixColumn++)
                {
                    matrix[matrixRow, matrixColumn] = source[rowIndex, columnIndex];
                }
            }

            return matrix;
        }
    }
}
