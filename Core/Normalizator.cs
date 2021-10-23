using NeuralNetworkLib.Abstractions;

namespace NeuralNetworkLib.Core
{
    public class Normalizator : IInputConverter
    {
        public double[,] Convert(double[,] inputs)
        {
            if (inputs is null)
                throw new ArgumentNullException(nameof(inputs));

            double[,] result = new double[inputs.GetLength(0), inputs.GetLength(1)];

            for (int column = 0; column < inputs.GetLength(1); column++)
            {

                double sum = 0.0;
                for (int row = 1; row < inputs.GetLength(0); row++)
                {
                    sum += inputs[row, column];
                }

                double average = sum / inputs.GetLength(0);
                double error = 0.0;
                for (int row = 1; row < inputs.GetLength(0); row++)
                {
                    error += Math.Pow(inputs[row, column] - average, 2);
                }

                double standardError = Math.Sqrt(error / inputs.GetLength(0));
                for (int row = 1; row < inputs.GetLength(0); row++)
                {
                    result[row, column] = (inputs[row, column] - average) / standardError;
                }
            }

            return result;
        }
    }
}
