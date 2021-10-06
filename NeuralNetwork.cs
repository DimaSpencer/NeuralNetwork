namespace NeuralNetwork
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
            if (_layerOfNeurons.Count != inputData.Count())
                throw new ArgumentOutOfRangeException(nameof(inputData));

            List<Neuron> firstNeurons = _layerOfNeurons[0].Neurons.ToList();
            firstNeurons.ForEach(n => n.FeedForward(inputData.ToArray()));

            for (int i = 1; i < _layerOfNeurons.Count(); i++)
            {
                _layerOfNeurons[i].ProcessLayer(_layerOfNeurons[0].NeuronOutputs);
                _layerOfNeurons[i].NeuronOutputs
            }
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
