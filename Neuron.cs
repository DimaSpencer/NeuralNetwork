namespace NeuralNetwork
{
    internal class Neuron
    {
        private readonly IActivationFunction _activationFunction;
        private List<double> _weights;

        public Neuron(int relationCount, IActivationFunction activationFunction)
        {
            #region ValidateInputData
            if (relationCount < 0)
                throw new ArgumentOutOfRangeException(nameof(relationCount));
            if (activationFunction is null)
                throw new ArgumentNullException(nameof(activationFunction));
            #endregion

            InitializeWeightCollection(collectionCount: relationCount, value: 0.5);

            _activationFunction = activationFunction;
        }

        public IEnumerable<double> Weights => _weights.AsReadOnly();
        public double Output { get; private set; }

        public void FeedForward(IEnumerable<double> input)//велечина активации (0-1)
        {
            if (_weights.Count() != input.Count())
                throw new ArgumentOutOfRangeException(nameof(input));

            int relationCount = input.Count();

            double sum = 0;
            for (int i = 0; i < relationCount; i++)
                sum += _weights[i] * input.ElementAt(i);

            Output = _activationFunction.Calculate(sum);
        }

        private void InitializeWeightCollection(int collectionCount, double value = 0.5)
        {
            if (collectionCount < 0)
                throw new ArgumentOutOfRangeException(nameof(collectionCount));

            _weights = new List<double>(collectionCount);
            _weights.ForEach(w => w = value);
        }
    }
}