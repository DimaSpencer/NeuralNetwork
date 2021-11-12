
using NeuralNetwork.Abstractions;
using NeuralNetworkLib.Extensions;
using System.Drawing;

namespace NeuralNetwork.Core
{
    public class SubsamplingLayer : IConvolutionalNeuralNetworkLayer
    {
        private List<double[,]> _output;

        public SubsamplingLayer(int kernelSize = 2)
        {
            if (kernelSize <= 1)
                throw new ArgumentOutOfRangeException(nameof(kernelSize));

            KernelSize = kernelSize;
            _output = new List<double[,]>();
        }

        public int KernelSize { get; private set; }
        public IReadOnlyList<double[,]> Output => _output.AsReadOnly();

        public IList<double[,]> ProcessCanals(IEnumerable<double[,]> canals)
        {
            //размер окна не соответствует изображению, будет слишком много пикселей, либо слишком мало
            if (canals.ElementAt(0).GetLength(0) % KernelSize != 0)
                throw new ArgumentNullException(nameof(KernelSize));
            if (canals.ElementAt(0).GetLength(1) % KernelSize != 0)
                throw new ArgumentNullException(nameof(KernelSize));

            //общие характеристики для всех каналов, потому что они все одинаковые в размерности
            double[,] firstCanal = canals.ElementAt(0);
            int rowCount = firstCanal.GetLength(0);
            int columnCount = firstCanal.GetLength(1);

            List<double[,]> result = new();

            foreach (var canal in canals)
            {
                int toRowIndex = rowCount - KernelSize;
                int toColumnIndex = columnCount - KernelSize;

                double[,] outputMap = new double[rowCount / KernelSize, columnCount / KernelSize];

                for (int rowIndex = 0, outputRowIndex = 0; rowIndex <= toRowIndex; rowIndex += KernelSize, outputRowIndex++)
                {
                    for (int columnIndex = 0, outputColumnIndex = 0; columnIndex <= toColumnIndex; columnIndex += KernelSize, outputColumnIndex++)
                    {
                        double[,] area = canal.GetAreaByIndex(new Point(columnIndex, rowIndex), KernelSize, KernelSize);
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
