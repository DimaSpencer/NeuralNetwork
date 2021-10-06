namespace NeuralNetwork
{
    public class NeuralNetworkSettings
    {
        public NeuralNetworkSettings(int inputNeuronsCount, int outputNeuronsCount, params int[] hiddenLayers)
        {
            if (inputNeuronsCount < 0)
                throw new ArgumentOutOfRangeException(nameof(inputNeuronsCount));
            if(outputNeuronsCount < 0)
                throw new ArgumentOutOfRangeException(nameof(outputNeuronsCount));
            if(hiddenLayers.Any(l => l < 0))
                throw new ArgumentOutOfRangeException(nameof(outputNeuronsCount));

            InputNeuronsCount = inputNeuronsCount;
            OutputNeuronsCount = outputNeuronsCount;
            HiddenLayers = new List<int>(hiddenLayers);

            if (HiddenLayers.Count() <= 0)
                throw new Exception();
        }

        public int InputNeuronsCount { get; }
        public int OutputNeuronsCount { get; }
        public  IEnumerable<int> HiddenLayers { get; }
    }
}
