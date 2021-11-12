using NeuralNetwork.Abstractions;

namespace NeuralNetwork.Core.Models
{
    public class ConvolutionalLayer : IConvolutionalNeuralNetworkLayer
    {
        private readonly List<Filter> _filters;
        private List<double[,]> _outputFeatureMaps;

        public ConvolutionalLayer(int kernelSize, int filtersCount, int inputCanals)
        {
            if (kernelSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(kernelSize), "Kernal size out of range");
            if (filtersCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(filtersCount), "Kernal count out of range");

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

        //TODO: тут подогнать метод под метод интерфейса
        public IList<double[,]> ProcessCanalsTwo(IEnumerable<double[,]> canals, int padding = 0, int stride = 1)
        {
            if(canals.Count() != _filters[0].Kernels.Count)
            if (canals is null)
                throw new ArgumentNullException(nameof(canals), "Input is null");
            if (padding < 0)
                throw new ArgumentOutOfRangeException(nameof(padding));
            if (stride <= 0)
                throw new ArgumentOutOfRangeException(nameof(stride));

            //TODO: добавить паддинг к входному изображения
            List<double[,]> featureMaps = new(_filters.Count);

            //обрабатываем каждый канали каждым ядром
            foreach (var filter in _filters)
            {
                double[,] map = filter.Process(canals, stride);

                featureMaps.Add(map);
            }

            return featureMaps;
        }

        public IList<double[,]> ProcessCanals(IEnumerable<double[,]> canals, int kernelSize)
        {
            throw new NotImplementedException();
        }
    }
}
