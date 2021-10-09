using NeuralNetwork.Maths;

namespace NeuralNetwork.Core
{
    public class Neuron
    {
        private readonly IActivationFunction _activationFunction;
        private List<double> _weights;
        private List<double> _inputs;

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

        public double Output { get; private set; }
        public IEnumerable<double> Inputs => _inputs.AsReadOnly();
        public IEnumerable<double> Weights => _weights.AsReadOnly();

        public void ProcessWeights(params double[] inputWeights)
        {
            if (_weights.Count() != inputWeights.Count())
                throw new ArgumentOutOfRangeException(nameof(inputWeights));

            _inputs = inputWeights.ToList();

            double sum = 0;
            for (int i = 0; i < inputWeights.Count(); i++)
                sum += _weights[i] * inputWeights.ElementAt(i);

            Output = _activationFunction.Calculate(sum);
        }

        public void ChangeWieght(double value, int forIndex)
        {

        }

        private void InitializeWeightCollection(int collectionCount, double value = 0.5)
        {
            _weights = new List<double>(collectionCount);

            for (int i = 0; i < collectionCount; i++)
                _weights.Add(value);
        }
    }
}