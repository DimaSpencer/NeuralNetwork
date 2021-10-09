using NeuralNetwork.Enums;
using NeuralNetwork.Maths;

namespace NeuralNetwork.Core
{
    public class NeuralNetwork : INeuralNetwork
    {   
        private List<LayerOfNeurons> _layerOfNeurons;

        public NeuralNetwork(NeuralNetworkSettings configuration)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            Configuration = configuration;
            _layerOfNeurons = new List<LayerOfNeurons>(Configuration.AllLayersCount);

            FillNetworkLayers();
        }

        public NeuralNetworkSettings Configuration { get; }
        public IEnumerable<LayerOfNeurons> LayerOfNeurons => _layerOfNeurons.AsReadOnly();

        public IEnumerable<double> ProcessData(IEnumerable<double> inputData)
        {
            if (_layerOfNeurons[0].NeuronsCount != inputData.Count())
                throw new ArgumentOutOfRangeException(nameof(inputData));

            var lastOutputs = inputData;
            for (int i = 0; i < _layerOfNeurons.Count; i++)
            {
                _layerOfNeurons[i].ProcessLayer(lastOutputs);
                lastOutputs = _layerOfNeurons[i].Outputs;
            }
            return lastOutputs;
        }

        private void FillNetworkLayers()
        {
            int previousNeuronsCount;
            int neuronsCount;

            previousNeuronsCount = 1;
            neuronsCount = Configuration.InputNeuronsCount;
            InitializeLayer(previousNeuronsCount, neuronsCount, NeuronType.Input);

            for (int i = 0; i < Configuration.HiddenLayers.Count(); i++)
            {
                previousNeuronsCount = _layerOfNeurons.Last().NeuronsCount;
                neuronsCount = Configuration.HiddenLayers.ElementAt(i);
                InitializeLayer(previousNeuronsCount, neuronsCount);
            }

            previousNeuronsCount = _layerOfNeurons.Last().NeuronsCount;
            neuronsCount = Configuration.OutputNeuronsCount;
            InitializeLayer(previousNeuronsCount, neuronsCount);
        }

        private void InitializeLayer(int previousNeuronsCount, int neuronsCount, NeuronType neuronType = NeuronType.Hidden)
        {
            IEnumerable<Neuron> neurons = CreateNeuronList(neuronsCount, previousNeuronsCount, Configuration.ActivationFunction);

            var layer = neuronType is NeuronType.Input ? new InputLayerOfNeurons(neurons) : new LayerOfNeurons(neurons);
            _layerOfNeurons.Add(layer);
        }

        private IEnumerable<Neuron> CreateNeuronList(int count, int previousNeuronCount, IActivationFunction activationFunction)
        {
            for (int i = 0; i < count; i++)
                yield return new Neuron(previousNeuronCount, activationFunction);
        }
    }
}
