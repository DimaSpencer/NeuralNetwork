
using NeuralNetwork.Abstractions;
using NeuralNetworkLib.Extensions;
using System.Drawing;

namespace NeuralNetwork.Core
{
    public class SubsamplingLayer : IConvolutionalNeuralNetworkLayer
    {
        private List<double[,]> _output;

        public SubsamplingLayer(int size = 2)
        {
            if (size <= 1)
                throw new ArgumentOutOfRangeException(nameof(size));

            Size = size;
            _output = new List<double[,]>();
        }

        public int Size { get; private set; }
        public IReadOnlyList<double[,]> Output => _output.AsReadOnly();

        public IList<double[,]> ProcessCanals(IEnumerable<double[,]> canals, int kernelSize)
        {
            if (kernelSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(kernelSize));
            //размер окна не соответствует изображению, будет слишком много пикселей, либо слишком мало
            if (canals.ElementAt(0).GetLength(0) % kernelSize != 0)
                throw new ArgumentNullException(nameof(kernelSize));
            if (canals.ElementAt(0).GetLength(1) % kernelSize != 0)
                throw new ArgumentNullException(nameof(kernelSize));

            //общие характеристики для всех каналов, потому что они все одинаковые в размерности
            double[,] firstCanal = canals.ElementAt(0);
            int rowCount = firstCanal.GetLength(0);
            int columnCount = firstCanal.GetLength(1);

            List<double[,]> result = new();

            foreach (var canal in canals)
            {
                int toRowIndex = rowCount - kernelSize;
                int toColumnIndex = columnCount - kernelSize;

                double[,] outputMap = new double[rowCount / kernelSize, columnCount / kernelSize];

                for (int rowIndex = 0, outputRowIndex = 0; rowIndex <= toRowIndex; rowIndex += kernelSize, outputRowIndex++)
                {
                    for (int columnIndex = 0, outputColumnIndex = 0; columnIndex <= toColumnIndex; columnIndex += kernelSize, outputColumnIndex++)
                    {
                        double[,] area = canal.GetAreaByIndex(new Point(columnIndex, rowIndex), kernelSize, kernelSize);
                        double maxValue = area.Cast<double>().Max();

                        outputMap[outputRowIndex, outputColumnIndex] = maxValue;
                    }
                }
                result.Add(outputMap);
            }

            return result;
        }
    }
}
