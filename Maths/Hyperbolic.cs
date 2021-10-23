using NeuralNetworkLib.Abstractions;
using NeuralNetworkLib.Maths;

namespace NeuralNetworkLib.Maths
{
    public class Hyperbolic : IActivationFunction
    {
        public double Calculate(double inputX)
        {
            return Math.Tanh(inputX);
        }
    }
}