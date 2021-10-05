namespace NeuralNetwork
{
    internal class SigmoidFunction : IActivationFunction
    {
        public double Calculate(double inputX)
        {
            return 1 / (1 + Math.Exp(-inputX));
        }
    }
}