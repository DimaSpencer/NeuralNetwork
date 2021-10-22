namespace NeuralNetworkLib.Core
{
    public static class WeightsInitializer
    {
        public static void InitializeNeuronsRandomValues(int weightCount, params Neuron[] neurons)
        {
            if (neurons is null)
                throw new ArgumentNullException(nameof(neurons));

            Random weightGenerator = new();

            for (int i = 0; i < neurons.Count(); i++)
            {
                for (int j = 0; j < weightCount; j++)
                {
                    neurons.ElementAt(i).ChangeWeight(weightGenerator.NextDouble(), j);
                }
            }
        }

        public static void InitializeNeuronsSpecificValues(int weightCount, double value, params Neuron[] neurons)
        {
            if (neurons is null)
                throw new ArgumentNullException(nameof(neurons));
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            for (int i = 0; i < neurons.Count(); i++)
            {
                for (int j = 0; j < weightCount; j++)
                {
                    neurons.ElementAt(i).ChangeWeight(value, j);
                }
            }
        }
    }
}