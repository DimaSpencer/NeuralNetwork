using NeuralNetwork.Maths;

namespace NeuralNetwork.Core
{
    public class NeuralNetwork
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

        public double ProcessData(IEnumerable<double> inputData)
        {
            if (_layerOfNeurons[0].NeuronsCount != inputData.Count())
                throw new ArgumentOutOfRangeException(nameof(inputData));

            var previousOutputs = inputData;
            for (int i = 0; i < _layerOfNeurons.Count; i++)
            {
                _layerOfNeurons[i].ProcessLayer(previousOutputs);
                previousOutputs = _layerOfNeurons[i].NeuronOutputs;
            }
            //previousOutputs это наш выход
        }

        private void FillNetworkLayers()
        {
            int previousNeuronsCount;
            int neuronsCount;

            previousNeuronsCount = 1;
            neuronsCount = Configuration.InputNeuronsCount;
            InitializeLayer(previousNeuronsCount, neuronsCount, NeuronsType.Input);

            for (int i = 0; i < Configuration.HiddenLayers.Count(); i++)
            {
                previousNeuronsCount = _layerOfNeurons.Last().NeuronsCount;
                neuronsCount = Configuration.HiddenLayers.ElementAt(i);
                InitializeLayer(previousNeuronsCount, neuronsCount, NeuronsType.Hidden);
            }

            previousNeuronsCount = _layerOfNeurons.Last().NeuronsCount;
            InitializeLayer(previousNeuronsCount, neuronsCount, NeuronsType.Output);
        }

        private void InitializeLayer(int previousNeuronsCount, int neuronsCount, NeuronsType neuronsType)
        {
            IEnumerable<Neuron> neurons = CreateNeuronList(neuronsCount, previousNeuronsCount, Configuration.ActivationFunction);
            _layerOfNeurons.Add(new LayerOfNeurons(neurons, neuronsType));
        }

        private IEnumerable<Neuron> CreateNeuronList(int count, int previousNeuronCount, IActivationFunction activationFunction)
        {
            for (int i = 0; i < count; i++)
                yield return new Neuron(previousNeuronCount, activationFunction);
        }
    }
}
