namespace NeuralNetwork.Core
{
    public class InputLayerOfNeurons : LayerOfNeurons
    {
        public InputLayerOfNeurons(IEnumerable<Neuron> neurons) : base(neurons)
        {
        }

        public override void ProcessLayer(IEnumerable<double> inputWeights)
        {
            if (inputWeights.Count() != _neurons.Count())
                throw new ArgumentOutOfRangeException(nameof(inputWeights));

            for (int i = 0; i < _neurons.Count; i++)
                _neurons[i].ProcessWeights(inputWeights.ElementAt(i));
        }
    }
}
