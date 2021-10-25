using NeuralNetworkLib.Abstractions;
using NeuralNetworkLib.Extensions;

namespace NeuralNetworkLib.Core
{
    public class Scaler : IInputConverter
    {
        public double[,] Convert(double[,] inputs)
        {
            if (inputs is null)
                throw new ArgumentNullException(nameof(inputs));

            var result = new double[inputs.GetLength(0), inputs.GetLength(1)];

            for (int column = 0; column < inputs.GetLength(1); column++)
            {
                double max = inputs.GetColumn(column).Max();
                double min = inputs.GetColumn(column).Min();

                var divider = max - min;

                for (int row = 0; row < inputs.GetLength(0); row++)
                {
                    result[row, column] = (inputs[row, column] - min) / divider;
                }
            }

            return result;
        }

        public Dataset Convert(Dataset dataset)
        {
            double[,] arrayInputs = ConvertTo2dArray(dataset.Inputs);
            double[,] expectedResults = ConvertTo2dArray(dataset.ExpectedResults);

            double[,] resultInputs = Convert(arrayInputs);

            Dataset newDataset = new Dataset(dataset.InputCount, dataset.ExpectedResultsCount);

            newDataset.InitializeFrom2dArray(resultInputs, expectedResults);

            return newDataset;
        }

        private double[,] ConvertTo2dArray(IEnumerable<double[]> input)
        {
            int columnCount = input.Count();
            int rowCount = input.ElementAt(0).Length;

            double[,] result = new double[columnCount, rowCount];

            for (int column = 0; column < columnCount; column++)
            {
                for (int row = 0; row < rowCount; row++)
                {
                    result[column, row] = input.ElementAt(column).ElementAt(row);
                }
            }
            return result;
        }
    }
}
