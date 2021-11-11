namespace NeuralNetwork.Core.Models
{
    public class LayerOf2DKernel
    {
        private readonly List<Filter> _filters;
        private List<double[,]> _outputFeatureMaps;
        public LayerOf2DKernel(int kernalSize, int kernalCount)
        {
            if (kernalSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(kernalSize), "Kernal size out of range");
            if (kernalSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(kernalCount), "Kernal count out of range");

            _filters = new List<Filter>(kernalCount);
            _outputFeatureMaps = new List<double[,]>(kernalCount);

            for (int i = 0; i < kernalCount; i++)
            {
                //создает нейрон с 3 фильтрами, ядрами, для 3 входных каналов
                Filter kernal = new(new List<double[,]>
                { 
                    new double[kernalSize, kernalSize], 
                    new double[kernalSize, kernalSize], 
                    new double[kernalSize, kernalSize]
                });

                kernal.InitializeRandomValues();
                _filters.Add(kernal);
            }
        }

        public IReadOnlyList<double[,]> OutputFeatureMaps => _outputFeatureMaps.AsReadOnly();

        public IList<double[,]> ProcessCanals(IEnumerable<double[,]> canals, int padding = 0, int stride = 1)
        {
            if (canals is null)
                throw new ArgumentNullException(nameof(canals), "Input is null");
            if (padding < 0)
                throw new ArgumentOutOfRangeException(nameof(padding));
            if (stride <= 0)
                throw new ArgumentOutOfRangeException(nameof(stride));

            //TODO: добавить паддинг к входному изображения
            List<double[,]> featureMaps = new(_filters.Count);

            //обрабатываем каждый канали каждым ядром
            foreach (var kernal in _filters)
            {
                double[,] map = kernal.Process(canals, stride);

                featureMaps.Add(map);
            }

            return featureMaps;
        }
    }
}
