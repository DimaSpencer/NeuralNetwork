using NeuralNetwork;

namespace NeuralNetwork.Core
{
    public class LayerOfNeurons
    {
        protected List<Neuron> _neurons;

        public LayerOfNeurons(IEnumerable<Neuron> neurons)
        {
            if (neurons is null)
                throw new ArgumentNullException(nameof(neurons));

            _neurons = neurons.ToList();
        }

        public IEnumerable<Neuron> Neurons => _neurons.AsReadOnly();
        public IEnumerable<double> Outputs => _neurons.Select(n => n.Output);

        public int NeuronsCount => _neurons.Count();

        public virtual void ProcessLayer(IEnumerable<double> inputWeights)
        {
            if (inputWeights.Count() != _neurons.First().Weights.Count())
                throw new ArgumentOutOfRangeException(nameof(inputWeights));

            _neurons.ForEach(n => n.ProcessWeights(inputWeights.ToArray()));
        }
    }
}
