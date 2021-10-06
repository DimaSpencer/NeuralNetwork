using NeuralNetwork.Enum;

namespace NeuralNetwork.Core
{
    public class LayerOfNeurons
    {
        private List<Neuron> _neurons;

        public LayerOfNeurons(IEnumerable<Neuron> neurons, NeuronsType neuronsType)
        {
            if (neurons is null)
                throw new ArgumentNullException(nameof(neurons));
            if (neurons is null)
                throw new ArgumentNullException(nameof(neurons));

            _neurons = neurons.ToList();
            NeuronsType = neuronsType;
        }

        public NeuronsType NeuronsType { get; }
        public IEnumerable<Neuron> Neurons => _neurons.AsReadOnly();
        public IEnumerable<double> NeuronOutputs => _neurons.Select(n => n.Output);

        public int NeuronsCount => _neurons.Count();

        public void ProcessLayer(IEnumerable<double> inputWeights)
        {
            if (inputWeights.Count() != _neurons.Count)
                throw new ArgumentOutOfRangeException(nameof(inputWeights));

            //ОЙ КАК НЕ НРАВИТСЯ 
            if (NeuronsType == NeuronsType.Input)
            {
                for (int i = 0; i < _neurons.Count; i++)
                    _neurons[i].FeedForward(inputWeights.ElementAt(i));
            }
            else
            {
                _neurons.ForEach(n => n.FeedForward(inputWeights.ToArray()));
            }
        }
    }
}
