using System;
using NeuralNetwork.Maths;

namespace NeuralNetwork.Maths
{
    public class Sigmoid : IActivationFunction
    {
        public double Calculate(double inputX)
        {
            return 1 / (1 + Math.Exp(-inputX));
        }
    }
}