using System.Runtime.Serialization;

namespace NeuralNetworkLib.Core
{
    [DataContract]
    public class LayerOfNeurons
    {
        [DataMember] protected List<Neuron> _neurons;

        public LayerOfNeurons(IEnumerable<Neuron> neurons)
        {
            if (neurons is null)
                throw new ArgumentNullException(nameof(neurons));

            _neurons = neurons.ToList();
        }

        public IReadOnlyCollection<Neuron> Neurons => _neurons.AsReadOnly();
        public IEnumerable<double> Outputs => _neurons.Select(n => n.Output);

        public int NeuronsCount => _neurons.Count;

        public virtual void ProcessLayer(IEnumerable<double> inputWeights)
        {
            if (inputWeights.Count() != _neurons.First().Weights.Count)
                throw new ArgumentOutOfRangeException(nameof(inputWeights));

            _neurons.ForEach(n => n.ProcessInputs(inputWeights.ToArray()));
        }
    }
}
