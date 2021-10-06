using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class LayerOfNeurons
    {
        private List<Neuron> _neurons;
        private NeuronsType _neuronsType;

        public LayerOfNeurons(IEnumerable<Neuron> neurons, NeuronsType neuronsType)
        {
            if (neurons is null)
                throw new ArgumentNullException(nameof(neurons));
            if (neurons is null)
                throw new ArgumentNullException(nameof(neurons));

            _neurons = neurons.ToList();
        }

        public IEnumerable<Neuron> Neurons => _neurons.AsReadOnly();
        public IEnumerable<double> NeuronOutputs => Neurons.Select(n => n.Output);
        public NeuronsType NeuronsType => _neuronsType;

        public int NeuronsCount => _neurons.Count();

        public void ProcessLayer(IEnumerable<double> inputWeights)
        {
            _neurons.ForEach(n => n.FeedForward(inputWeights.ToArray()));
        }
    }
}
