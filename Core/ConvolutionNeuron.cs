using NeuralNetworkLib.Abstractions;

namespace NeuralNetwork.Core.Models
{
    public class ConvolutionNeuron
    {
        private readonly IActivationFunction _activationFunction;
        private double[,] _convolutionCore;

        public ConvolutionNeuron(double[,] convolutionCore, IActivationFunction activationFunction)
        {
            if (convolutionCore is null)
                throw new ArgumentNullException(nameof(convolutionCore), "ConvolutionCore is null");
            if (activationFunction is null)
                throw new ArgumentNullException(nameof(activationFunction), "Activation function is null");

            _convolutionCore = convolutionCore;
            _activationFunction = activationFunction;
        }

        public double Output { get; private set; }

        public void SetConvotutionCore(double[,] convolutionCore)
        {
            if (convolutionCore is null)
                throw new ArgumentNullException(nameof(convolutionCore), "ConvolutionCore is null");

            _convolutionCore = convolutionCore;
        }

        public double CalculateConvolution(double[,] input)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input is null");

            int rowCount = input.GetLength(0);
            int coulumnCount = input.GetLength(1);

            double result = 0.0;

            for (int column = 0; column < coulumnCount; column++)
            {
                for (int row = 0; row < rowCount; row++)
                {
                    double value = _convolutionCore[column, row] * input[column, row];
                    result += value;
                }
            }

            //Output = _activationFunction.Calculate(result);
            Output = result;
            return Output;
        }
    }
}
