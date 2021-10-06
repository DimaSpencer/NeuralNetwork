using System;
using NeuralNetwork.Maths;

namespace NeuralNetwork
{
    public class Sigmoid : IActivationFunction
    {
        public double Calculate(double inputX)
        {
            return 1 / (1 + Math.Exp(-inputX));
        }
    }
}