using NeuralNetworkLib.Abstractions;

namespace NeuralNetworkLib.Core
{
    public class Dataset
    {
        private Dictionary<double[], double[]> _datasets;
        private readonly int _inputCount;
        private readonly int _expectedResultsCount;
        private readonly IInputConverter _converter;

        public Dataset(int inputCount, int expectedResultsCount, IInputConverter? converter = null)
        {
            if (inputCount < 0)
                throw new ArgumentOutOfRangeException(nameof(inputCount));
            if (expectedResultsCount < 0)
                throw new ArgumentOutOfRangeException(nameof(expectedResultsCount));

            if (converter is null)
                converter = new Scaler();

            _datasets = new Dictionary<double[], double[]>();
            _inputCount = inputCount;
            _expectedResultsCount = expectedResultsCount;
            _converter = converter;
        }

        public IReadOnlyDictionary<double[], double[]> Sets => _datasets;
        public IReadOnlyCollection<double[]> Inputs => _datasets.Keys;
        public IReadOnlyCollection<double[]> ExpectedResults => _datasets.Values;

        public void InitializeFrom2dArray(double[,] inputSets, double[,] expectedResults)
        {
            if (inputSets is null)
                throw new ArgumentNullException(nameof(inputSets));
            if (inputSets.GetLength(1) != _inputCount)
                throw new ArgumentOutOfRangeException(nameof(inputSets));

            if (expectedResults is null)
                throw new ArgumentNullException(nameof(expectedResults));
            if (expectedResults.GetLength(1) != _expectedResultsCount)
                throw new ArgumentOutOfRangeException(nameof(expectedResults));

            _datasets.Clear();

            inputSets = _converter.Convert(inputSets);

            for (int row = 0; row < inputSets.GetLength(0); row++)
            {
                var inputs = Enumerable.Range(0, inputSets.GetLength(1))
                    .Select(x => inputSets[row, x])
                    .ToArray();

                var eResults = Enumerable.Range(0, expectedResults.GetLength(1))
                    .Select(x => expectedResults[row, x])
                    .ToArray();

                _datasets.Add(inputs, eResults);
            }
        }

        public void Add(double[] inputs, double[] expectedResults)
        {
            if (inputs is null)
                throw new ArgumentNullException(nameof(inputs));
            if (inputs.Count() != _inputCount)
                throw new ArgumentOutOfRangeException(nameof(inputs));

            if(expectedResults is null)
                throw new ArgumentNullException(nameof(expectedResults));
            if (expectedResults.Count() != _expectedResultsCount)
                throw new ArgumentOutOfRangeException(nameof(expectedResults));

            _datasets.Add(inputs, expectedResults);
        }
    }
}
