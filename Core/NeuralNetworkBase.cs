using NeuralNetworkLib.Abstractions;
using NeuralNetworkLib.Enums;
using NeuralNetworkLib.Maths;

namespace NeuralNetworkLib.Core
{
    public class NeuralNetworkBase : INeuralNetwork
    {   
        private List<LayerOfNeurons> _layerOfNeurons;

        public NeuralNetworkBase(Action<NeuralNetworkSettings> configuration)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            Configuration = new NeuralNetworkSettings( 0, 0, 0);
            configuration.Invoke(Configuration);

            _layerOfNeurons = new List<LayerOfNeurons>(Configuration.AllLayersCount);
            FillNetworkLayers();
        }
        public NeuralNetworkBase(NeuralNetworkSettings configuration)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            Configuration = configuration;

            _layerOfNeurons = new List<LayerOfNeurons>(Configuration.AllLayersCount);
            FillNetworkLayers();
        }

        public NeuralNetworkSettings Configuration { get; }
        public IEnumerable<LayerOfNeurons> LayerOfNeurons => _layerOfNeurons.AsReadOnly();

        public IEnumerable<double> ProcessData(IEnumerable<double> inputData) //IDataset
        {
            if (_layerOfNeurons[0].NeuronsCount != inputData.Count())
                throw new ArgumentOutOfRangeException(nameof(inputData));

            List<double> lastOutputs = inputData.ToList();
            for (int i = 0; i < _layerOfNeurons.Count; i++)
            {
                _layerOfNeurons[i].ProcessLayer(lastOutputs);
                lastOutputs = _layerOfNeurons[i].Outputs.ToList();
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
            IActivationFunction activationFunction = Configuration.ActivationFunction switch
            {
                ActivationFunction.Sigmoid => new Sigmoid(),
                ActivationFunction.Hyperbolic => new Hyperbolic(),
                _ => new Sigmoid(),
            };

            Neuron[] neurons = CreateNeuronList(neuronsCount, previousNeuronsCount, activationFunction);

            int weightCount = neurons.First().Weights.Count();
            LayerOfNeurons layer;
            if (neuronType == NeuronType.Input)
            {
                WeightsInitializer.InitializeNeuronsSpecificValues(weightCount, 1, neurons);
                layer = new InputLayerOfNeurons(neurons);
            }
            else
            {
                WeightsInitializer.InitializeNeuronsRandomValues(weightCount, neurons);
                layer = new LayerOfNeurons(neurons);
            }

            _layerOfNeurons.Add(layer);
        }

        private Neuron[] CreateNeuronList(int count, int previousNeuronCount, IActivationFunction activationFunction)
        {
            Neuron[] neurons = new Neuron[count];
            for (int i = 0; i < count; i++)
                neurons[i] = new Neuron(previousNeuronCount, activationFunction);

            return neurons;
        }
    }
}
