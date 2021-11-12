using NeuralNetwork.Abstractions;
using System.Collections.Generic;

namespace NeuralNetwork.Core.Models
{
    public class ConvolutionalLayer : IConvolutionalNeuralNetworkLayer
    {
        private readonly int _padding;
        private readonly int _stride;

        private readonly List<Filter> _filters;
        private List<double[,]> _outputFeatureMaps;

        public ConvolutionalLayer(int kernelSize, int filtersCount, int inputCanals, int padding = 0, int stride = 1)
        {
            if (kernelSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(kernelSize), "Kernal size out of range");
            if (filtersCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(filtersCount), "Kernal count out of range");
            if (padding < 0)
                throw new ArgumentOutOfRangeException(nameof(padding));
            if (stride <= 0)
                throw new ArgumentOutOfRangeException(nameof(stride));
            
            _padding = padding;
            _stride = stride;
            
            _filters = new List<Filter>(filtersCount);
            _outputFeatureMaps = new List<double[,]>(filtersCount);

            for (int i = 0; i < filtersCount; i++)
            {
                //создает фильтр с ядрами, количество ядер зависит от количества превыдущих каналов
                List<double[,]> canals = new();
                for (int c = 0; c < inputCanals; c++)
                    canals.Add(new double[kernelSize, kernelSize]);

                Filter filter = new(canals);

                filter.InitializeRandomValues();
                _filters.Add(filter);
            }
        }

        public IReadOnlyList<double[,]> Output => _outputFeatureMaps.AsReadOnly();

        public IList<double[,]> ProcessCanals(IEnumerable<double[,]> canals)
        {
            if (canals.Count() != _filters[0].Kernels.Count)
                throw new ArgumentException("Input channels must be equal to the number of kernels", nameof(canals));
            if (canals is null)
                throw new ArgumentNullException(nameof(canals), "Input is null");

            List<double[,]> featureMaps = new(_filters.Count);

            if (_padding > 0)
                canals = AddPadding(canals);

            foreach (var filter in _filters)
            {
                double[,] map = filter.Process(canals, _stride);
                featureMaps.Add(map);
            }

            return featureMaps;
        }

        private List<double[,]> AddPadding(IEnumerable<double[,]> canals)
        {
            List<double[,]> output = canals.ToList();

            for (int i = 0; i < output.Count; i++)
            {
                int rows = output[i].GetLength(0);
                int columns = output[i].GetLength(1);

                int newRows = rows + _padding * 2;
                int newColumns = columns + _padding * 2;

                double[,] newArray = new double[newRows, newColumns];
                for (int m = _padding, canalRowIndex = 0; canalRowIndex < rows; m++, canalRowIndex++)
                    for (int n = _padding, canalColumnIndex = 0; canalColumnIndex < columns; n++, canalColumnIndex++)
                        newArray[m, n] = output[i][canalRowIndex, canalColumnIndex];

                output[i] = newArray;
            }

            return output;
        }
    }
}
