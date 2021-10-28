using NeuralNetworkLib.Abstractions;
using NeuralNetworkLib.Enums;
using NeuralNetworkLib.Maths;
using System;
using System.Runtime.Serialization;

namespace NeuralNetworkLib.Core
{
    [DataContract]
    [KnownType(typeof(List<>))]
    [KnownType(typeof(Neuron))]
    [KnownType(typeof(LayerOfNeurons))]
    [KnownType(typeof(InputLayerOfNeurons))]
    [KnownType(typeof(NeuralNetworkSettings))]
    public class NeuralNetworkBase : INeuralNetwork
    {   
        [DataMember] private List<LayerOfNeurons> _layerOfNeurons;

        public NeuralNetworkBase(Action<NeuralNetworkSettings> configuration)
        {
            Configuration = new NeuralNetworkSettings( 0, 0, 0);
            configuration?.Invoke(Configuration);

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

        [DataMember] public NeuralNetworkSettings Configuration { get; private set; }
        public IReadOnlyCollection<LayerOfNeurons> LayerOfNeurons => _layerOfNeurons.AsReadOnly();

        public async Task SaveAsync(string filePath, ISerializer serializer)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("file path is null", nameof(filePath));
            if (serializer is null)
                throw new ArgumentNullException(nameof(serializer)); string xmlData = serializer.Serialize(this);

            await FileLoader.SaveAsync(filePath, xmlData);
        }

        public static async Task<NeuralNetworkBase> LoadInstanceAsync(string source, ISerializer serializer)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentException("source is null", nameof(source));
            if (serializer is null)
                throw new ArgumentNullException(nameof(serializer));

            string xmlData = await FileLoader.LoadAsync(source);
            return serializer.Deserialize<NeuralNetworkBase>(xmlData);
        }

        public IEnumerable<double> ProcessData(IEnumerable<double> inputData)
        {
            if (_layerOfNeurons[0].NeuronsCount != inputData.Count())
                throw new ArgumentOutOfRangeException(nameof(inputData));

            IEnumerable<double> lastOutputs = inputData;
            for (int i = 1; i < _layerOfNeurons.Count; i++)
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
            IActivationFunction activationFunction = Configuration.ActivationFunction switch
            {
                ActivationFunction.Sigmoid => new Sigmoid(),
                ActivationFunction.Hyperbolic => new Hyperbolic(),
                _ => new Sigmoid(),
            };

            Neuron[] neurons = CreateNeuronList(neuronsCount, previousNeuronsCount, activationFunction);

            int weightCount = neurons.First().Weights.Count;
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
