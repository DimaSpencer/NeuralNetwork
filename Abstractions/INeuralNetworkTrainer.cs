namespace NeuralNetwork.Core
{
    public interface INeuralNetworkTrainer
    {
        void Train(IEnumerable<double> inputs, double expectedResult)
    }
}