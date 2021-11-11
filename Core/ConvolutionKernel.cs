using NeuralNetworkLib.Abstractions;
using NeuralNetworkLib.Core;
using NeuralNetworkLib.Maths;
using System.Drawing;

namespace NeuralNetwork.Core.Models
{
    public class ConvolutionKernel
    {
        private double[,] _filter;
        private List<ConvolutionNeuron> _neurons;

        public ConvolutionKernel(double[,] filter)
        {
            _filter = filter;
        }

        public double[,] OutputMap { get; private set; }

        public void InitializeRandomValues()
        {
            WeightsInitializer.InitializeMatrixKernalRandomValues(_filter);
        }

        public double[,] Process(double[,] image, int stride)
        {
            int rowCount = image.GetLength(0);
            int columnCount = image.GetLength(1);
            int neuronCount = (rowCount - 1) * (columnCount - 1);

            double[,] result = new double[rowCount - 1, columnCount - 1];

            _neurons = new List<ConvolutionNeuron>(neuronCount);

            for (int i = 0; i < neuronCount; i++)
                _neurons.Add(new ConvolutionNeuron(_filter, new Sigmoid()));

            int toRowIndex = rowCount - _filter.GetLength(0);
            int toColumnIndex = columnCount - _filter.GetLength(1);

            for (int rowIndex = 0; rowIndex < toRowIndex; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < toColumnIndex; columnIndex++)
                {
                    ConvolutionNeuron currentNeuron = _neurons[columnIndex + rowIndex];

                    double[,] partial = TakeAreaByIndex(image, new Point(columnIndex, rowIndex), _filter.GetLength(1), _filter.GetLength(0));
                    double output = currentNeuron.CalculateConvolution(partial);

                    result[rowIndex, columnIndex] = output;
                }
            }

            OutputMap = result;
            return result;
        }

        private double[,] TakeAreaByIndex(double[,] source, Point index3D, int width, int height)
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
