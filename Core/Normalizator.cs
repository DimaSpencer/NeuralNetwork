using NeuralNetworkLib.Abstractions;
using NeuralNetworkLib.Extensions;

namespace NeuralNetworkLib.Core
{
    public class Normalizator : IInputConverter
    {
        public double[,] Convert(double[,] inputs)
        {
            var result = new double[inputs.GetLength(0), inputs.GetLength(1)];

            for (int column = 0; column < inputs.GetLength(1); column++)
            {
                // Среднее значение сигнала нейрона.
                var sum = 0.0;
                for (int row = 0; row < inputs.GetLength(0); row++)
                {
                    sum += inputs[row, column];
                }

                var averageValue = inputs.GetRow(column).Average();
                var average = sum / inputs.GetLength(0);

                // Стандартное квадратичное отклонение нейрона.
                var error = 0.0;
                for (int row = 0; row < inputs.GetLength(0); row++)
                {
                    error += Math.Pow((inputs[row, column] - average), 2);
                }
                var standardError = Math.Sqrt(error / inputs.GetLength(0));

                for (int row = 0; row < inputs.GetLength(0); row++)
                {
                    result[row, column] = (inputs[row, column] - average) / standardError;
                }
            }
            return result;
        }

        public Dataset Convert(Dataset inputs)
        {
            throw new NotImplementedException();
        }
    }
}
