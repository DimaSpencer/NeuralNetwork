﻿using NeuralNetworkLib.Abstractions;

namespace NeuralNetworkLib.Core
{
    public class Neuron
    {
        private readonly IActivationFunction _activationFunction;
        private double[] _weights;
        private double[] _inputs;

        public Neuron(int relationCount, IActivationFunction activationFunction)
        {
            #region ValidateInputData
            if (relationCount < 0)
                throw new ArgumentOutOfRangeException(nameof(relationCount));
            if (activationFunction is null)
                throw new ArgumentNullException(nameof(activationFunction));
            #endregion

            _weights = new double[relationCount];
            _inputs = new double[relationCount];

            _activationFunction = activationFunction;
        }

        public double Output { get; private set; }
        public double Error { get; set; }
        public IEnumerable<double> Inputs => _inputs;
        public IEnumerable<double> Weights => _weights;

        //временное решение, обязательно поменять, потому что аутпут при обработке сигмоиды при вводе 0 будет всегда 0.5, мы это обходим этим методом
        public void SetInputs(params double[] inputs)
        {
            if (inputs is null)
                throw new ArgumentNullException(nameof(inputs));

            _inputs = inputs;
            Output = inputs.Average();
        }

        public double ProcessWeights(params double[] inputWeights)
        {
            if (_inputs.Length != inputWeights.Count())
                throw new ArgumentOutOfRangeException(nameof(inputWeights));

            _inputs = inputWeights;

            double sum = 0;
            for (int i = 0; i < inputWeights.Count(); i++)
                sum += _weights[i] * inputWeights.ElementAt(i);

            return Output = _activationFunction.Calculate(sum);
        }

        public void ChangeWeight(double value, int byIndex)
        {
            if (byIndex < 0 || _weights.Length - 1 < byIndex)
                throw new IndexOutOfRangeException(nameof(byIndex));

            _weights[byIndex] = value;
        }
    }
}