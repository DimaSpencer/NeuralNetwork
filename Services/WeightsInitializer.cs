using NeuralNetworkLib.Extensions;

namespace NeuralNetworkLib.Core
{
    public static class WeightsInitializer
    {
        public static void InitializeNeuronsRandomValues(int weightCount, params Neuron[] neurons)
        {
            if (neurons is null)
                throw new ArgumentNullException(nameof(neurons));

            Random weightGenerator = new();

            for (int i = 0; i < neurons.Length; i++)
            {
                for (int j = 0; j < weightCount; j++)
                {
                    neurons.ElementAt(i).ChangeWeight(weightGenerator.NextDouble(-0.5, 0.5), j);
                }
            }
        }

        public static void InitializeNeuronsSpecificValues(int weightCount, double value, params Neuron[] neurons)
        {
            if (neurons is null)
                throw new ArgumentNullException(nameof(neurons));
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            for (int i = 0; i < neurons.Length; i++)
            {
                for (int j = 0; j < weightCount; j++)
                {
                    neurons.ElementAt(i).ChangeWeight(value, j);
                }
            }
        }

        public static void InitializeMatrixKernelRandomValues(double[,] kernel)
        {
            if (kernel is null)
                throw new ArgumentNullException(nameof(kernel), "Matrix is null");

            Random weightGenerator = new();
            int rowLength = kernel.GetLength(0);
            int columnLength = kernel.GetLength(1);

            for (int rowIndex = 0; rowIndex < rowLength; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columnLength; columnIndex++)
                {
                    kernel[rowIndex, columnIndex] = weightGenerator.Next(-10, 10);
                }
            }
        }
    }
}