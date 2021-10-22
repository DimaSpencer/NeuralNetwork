namespace NeuralNetworkLib.Maths
{
    public static class NeuralNetworkMath
    {
        public static double CalculateDerivative(double x)
        {
            return x / (1 - x);
        }
    }
}