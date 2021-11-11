using NeuralNetworkLib.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NeuralNetworkLib.Core
{
    [DataContract]
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

        [Range(1, int.MaxValue)]
        [DataMember] public int InputNeuronsCount { get; set; }

        [Range(1, int.MaxValue)]
        [DataMember] public int OutputNeuronsCount { get; set; }

        [DataMember] public IEnumerable<int> HiddenLayers { get; set; }
        [DataMember] public ActivationFunction ActivationFunction { get; set; }

        public int AllLayersCount => HiddenLayers.Count() + 2;
    }
}
