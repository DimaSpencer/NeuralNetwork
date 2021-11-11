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

        public static void InitializeMatrixCoreRandomValues(double[,] core)
        {
            if (core is null)
                throw new ArgumentNullException(nameof(core), "Matrix is null");

            int rowLength = core.GetLength(0);
            int columnLength = core.GetLength(1);
            for (int rowIndex = 0; rowIndex < core.GetLength(1); rowIndex++)
            {

            }
        }
    }
}