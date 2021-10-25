using NeuralNetworkLib.Core;

namespace NeuralNetworkLib.Abstractions
{
    public interface IInputConverter
    {
        double[,] Convert(double[,] inputs);
        Dataset Convert(Dataset dataset);
    }
}
