using NeuralNetworkLib.Core;

namespace NeuralNetworkLib.Abstractions
{
    public interface INeuralNetworkTrainer
    {
        void StudyingAtDataset(Dataset dataset, int epoch);
    }
}