namespace NeuralNetworkLib.Abstractions
{
    public interface IInputConverter
    {
        double[,] Convert(double[,] inputs);
    }
}
