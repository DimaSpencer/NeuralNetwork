using NeuralNetworkLib.Abstractions;

namespace NeuralNetworkLib.Core
{
    public class Scaler : IInputConverter
    {
        public double[,] Convert(double[,] inputs)
        {
            if (inputs is null)
                throw new ArgumentNullException(nameof(inputs));

            double[,] result = new double[inputs.GetLength(0), inputs.GetLength(1)];

            for (int column = 0; column < inputs.GetLength(1); column++)
            {
                double min = inputs[0, column];
                double max = inputs[0, column];

                for (int row = 1; row < inputs.GetLength(0); row++)
                {
                    double item = inputs[row, column];

                    if(item < min)
                    {
                        min = item;
                    }
                    if(item > max)
                    {
                        max = item;
                    }
                }

                double divider = max - min;
                for (int row = 1; row < inputs.GetLength(0); row++)
                {
                    result[row, column] = (inputs[row, column] - min) / divider;
                }
            }

            return result;
        }
    }
}
