using NeuralNetwork.Maths;

namespace NeuralNetwork.Maths
{
    public class Hyperbolic : IActivationFunction
    {
        public double Calculate(double inputX)
        {
            return Math.Tanh(inputX);
        }
    }
}