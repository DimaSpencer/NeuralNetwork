using System.Runtime.Serialization;
using System.Xml;

namespace NeuralNetworkLib.Extensions
{
    public static class RandomExtensions
    {
        public static double NextDouble(this Random RandGenerator, double minValue, double maxValue)
        {
            return RandGenerator.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}