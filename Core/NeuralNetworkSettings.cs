using NeuralNetworkLib.Abstractions;
using NeuralNetworkLib.Enums;
using NeuralNetworkLib.Maths;

namespace NeuralNetworkLib.Core
{
    public class NeuralNetworkSettings
    {
        public NeuralNetworkSettings(int inputNeuronsCount, int outputNeuronsCount, ActivationFunction activationFunction = ActivationFunction.Sigmoid, params int[] hiddenLayers)
        {
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

        public ActivationFunction ActivationFunction { get; set; }
        public int InputNeuronsCount { get; set; }
        public int OutputNeuronsCount { get; set; }
        public  IEnumerable<int> HiddenLayers { get; set; }

        public int AllLayersCount => HiddenLayers.Count() + 2;
    }
}
