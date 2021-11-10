using NeuralNetworkLib.Extensions;

namespace NeuralNetworkLib.Core
{
    [Serializable]
    public class Dataset
    {
        private Dictionary<IEnumerable<double>, IEnumerable<double>> _datasets;

        public Dataset(int inputCount, int expectedResultsCount)
        {
            if (inputCount < 0)
                throw new ArgumentOutOfRangeException(nameof(inputCount));
            if (expectedResultsCount < 0)
                throw new ArgumentOutOfRangeException(nameof(expectedResultsCount));

            InputCount = inputCount;
            ExpectedResultsCount = expectedResultsCount;
            
            _datasets = new Dictionary<IEnumerable<double>, IEnumerable<double>>();
        }

        public int InputCount { get; }
        public int ExpectedResultsCount { get; }
        public IReadOnlyDictionary<IEnumerable<double>, IEnumerable<double>> Sets => _datasets;
        public IReadOnlyCollection<IEnumerable<double>> Inputs => _datasets.Keys;
        public IReadOnlyCollection<IEnumerable<double>> ExpectedResults => _datasets.Values;

        public void InitializeFrom2dArray(double[,] inputs, double[,] expectedResults)
        {
            double inputColumnLength = inputs.GetLength(1);
            double inputRowLength = inputs.GetLength(0);

            if (inputRowLength != expectedResults.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(inputs));

            if (inputs is null)
                throw new ArgumentNullException(nameof(inputs));
            if (inputColumnLength != InputCount)
                throw new ArgumentOutOfRangeException(nameof(inputs));

            if (expectedResults is null)
                throw new ArgumentNullException(nameof(expectedResults));
            if (expectedResults.GetLength(1) != ExpectedResultsCount)
                throw new ArgumentOutOfRangeException(nameof(expectedResults));

            _datasets.Clear();

            for (int row = 0; row < inputRowLength; row++)
            {
                var inputRows = inputs.GetRow(row);
                var expectedRows = expectedResults.GetRow(row);

                _datasets.Add(inputRows, expectedRows);
            }
        }

        public void Add(IEnumerable<double> inputs, IEnumerable<double> expectedResults)
        {
            if (inputs is null)
                throw new ArgumentNullException(nameof(inputs));
            if (inputs.Count() != InputCount)
                throw new ArgumentOutOfRangeException(nameof(inputs));

            if(expectedResults is null)
                throw new ArgumentNullException(nameof(expectedResults));
            if (expectedResults.Count() != ExpectedResultsCount)
                throw new ArgumentOutOfRangeException(nameof(expectedResults));

            _datasets.Add(inputs, expectedResults);
        }
    }
}
