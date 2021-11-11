using System.Runtime.Serialization;

namespace NeuralNetworkLib.Core
{
    [DataContract]
    public class InputLayerOfNeurons : LayerOfNeurons
    {
        public InputLayerOfNeurons(IEnumerable<Neuron> neurons) : base(neurons)
        {
        }

        public override void ProcessLayer(IEnumerable<double> input)
        {
            if (input.Count() != _neurons.Count)
                throw new ArgumentOutOfRangeException(nameof(input));

            for (int i = 0; i < _neurons.Count; i++)
                _neurons[i].SetInputs(input.ElementAt(i));
        }
    }
}
