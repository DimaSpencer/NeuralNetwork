namespace NeuralNetworkLib.Abstractions
{
    public interface INeuralNetworkTrainer
    {
        void Train(IEnumerable<double> inputs, IEnumerable<double> expectedResults);
    }
}