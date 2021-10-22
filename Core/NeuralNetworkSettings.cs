using NeuralNetworkLib.Maths;

namespace NeuralNetworkLib.Core
{
    public class NeuralNetworkSettings
    {
        public NeuralNetworkSettings(IActivationFunction activationFunction, int inputNeuronsCount, int outputNeuronsCount, params int[] hiddenLayers)
        {
            if (activationFunction is null)
                throw new ArgumentNullException(nameof(activationFunction));
            if (inputNeuronsCount < 0)
                throw new ArgumentOutOfRangeException(nameof(inputNeuronsCount));
            if(outputNeuronsCount < 0)
                throw new ArgumentOutOfRangeException(nameof(outputNeuronsCount));
            if(hiddenLayers.Any(l => l < 0))
                throw new ArgumentOutOfRangeException(nameof(outputNeuronsCount));

            ActivationFunction = activationFunction;
            InputNeuronsCount = inputNeuronsCount;
            OutputNeuronsCount = outputNeuronsCount;
            HiddenLayers = new List<int>(hiddenLayers);
        }

        public IActivationFunction ActivationFunction { get; }
        public int InputNeuronsCount { get; }
        public int OutputNeuronsCount { get; }
        public  IEnumerable<int> HiddenLayers { get; }

        public int AllLayersCount => HiddenLayers.Count() + 2;
    }
}
