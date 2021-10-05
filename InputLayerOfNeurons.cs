using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    internal class InputLayerOfNeurons : ILayerOfNeurons
    {
        private List<Neuron> _neurons;

        public InputLayerOfNeurons(IEnumerable<Neuron> neurons)
        {
            if (neurons is null)
                throw new ArgumentNullException(nameof(neurons));

            _neurons = neurons;
        }

        public IEnumerable<Neuron> Neurons => _neurons.AsReadOnly();
    }
}
