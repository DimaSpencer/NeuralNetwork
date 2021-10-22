using System;

namespace NeuralNetworkLib.Maths
{
    public class Sigmoid : IActivationFunction
    {
        public double Calculate(double inputX)
        {
            return 1.0 / (1.0 + Math.Exp(-inputX));
        }
    }
}