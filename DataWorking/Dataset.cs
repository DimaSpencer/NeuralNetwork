﻿using NeuralNetworkLib.Abstractions;
using NeuralNetworkLib.Extensions;

namespace NeuralNetworkLib.Core
{
    [Serializable]
    public class Dataset
    {
        private Dictionary<double[], double[]> _datasets;
        private readonly IInputConverter _converter;

        public Dataset(int inputCount, int expectedResultsCount, IInputConverter? converter = null)
        {
            if (inputCount < 0)
                throw new ArgumentOutOfRangeException(nameof(inputCount));
            if (expectedResultsCount < 0)
                throw new ArgumentOutOfRangeException(nameof(expectedResultsCount));

            if (converter is null)
                converter = new Scaler();

            InputCount = inputCount;
            ExpectedResultsCount = expectedResultsCount;
            
            _datasets = new Dictionary<double[], double[]>();
            _converter = converter;
        }

        public int InputCount { get; }
        public int ExpectedResultsCount { get; }
        public IReadOnlyDictionary<double[], double[]> Sets => _datasets;
        public IReadOnlyCollection<double[]> Inputs => _datasets.Keys;
        public IReadOnlyCollection<double[]> ExpectedResults => _datasets.Values;

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

        public void Add(double[] inputs, double[] expectedResults)
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
