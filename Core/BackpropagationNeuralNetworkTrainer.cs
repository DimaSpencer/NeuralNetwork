using NeuralNetworkLib.Abstractions;
using NeuralNetworkLib.Maths;

namespace NeuralNetworkLib.Core
{
    public class BackpropagationTrainer : INeuralNetworkTrainer
    {
        private readonly INeuralNetwork _neuralNetworkStudent;
        private double _learningRate = 0.1;

        public BackpropagationTrainer(INeuralNetwork neuralNetworkStudent)
        {
            if (neuralNetworkStudent is null)
                throw new ArgumentNullException(nameof(neuralNetworkStudent));
            

            _neuralNetworkStudent = neuralNetworkStudent;
        }

        public double LearningRate
        {
            get => _learningRate;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                _learningRate = value;
            }
        }

        public void StudyingAtDataset(Dataset dataset, int epoch)
        {
            if (dataset is null)
                throw new ArgumentNullException(nameof(dataset));
            if (epoch < 0)
                throw new ArgumentOutOfRangeException(nameof(epoch));

            for (int i = 0; i < epoch; i++)
            {
                for (int j = 0; j < dataset.Sets.Count; j++)
                {
                    var inputs = dataset.Sets.ElementAt(j).Key;
                    var expectedResults = dataset.Sets.ElementAt(j).Value;
                    TrainSet(inputs, expectedResults);
                }
            }
        }

        private void TrainSet(IEnumerable<double> inputs, IEnumerable<double> expectedResults)
        {
            if (inputs is null)
                throw new ArgumentNullException(nameof(inputs));
            if(_neuralNetworkStudent.LayerOfNeurons.First().NeuronsCount != inputs.Count())
                throw new ArgumentOutOfRangeException(nameof(inputs));
            if (_neuralNetworkStudent.LayerOfNeurons.Last().NeuronsCount != expectedResults.Count())
                throw new ArgumentOutOfRangeException(nameof(inputs));

            var actualResults = _neuralNetworkStudent.ProcessData(inputs);

            //для последнего результирующего слоя
            var lastNeurons = _neuralNetworkStudent.LayerOfNeurons.Last().Neurons;
            for (int i = 0; i < expectedResults.Count(); i++)
            {
                Neuron currentNeuron = lastNeurons.ElementAt(i);

                double error = actualResults.ElementAt(i) - expectedResults.ElementAt(i);
                currentNeuron.Error = error;

                TrainNeuron(currentNeuron);
            }

            //для остальных нейронов
            for (int i = _neuralNetworkStudent.LayerOfNeurons.Count() - 2; i >= 0; i--)
            {
                LayerOfNeurons currentLayer = _neuralNetworkStudent.LayerOfNeurons.ElementAt(i);
                LayerOfNeurons previousLayer = _neuralNetworkStudent.LayerOfNeurons.ElementAt(i + 1);

                for (int j = 0; j < currentLayer.NeuronsCount; j++)
                {
                    Neuron currentNeuron = currentLayer.Neurons.ElementAt(j);
                    double error = 0;

                    for (int k = 0; k < previousLayer.Neurons.Count(); k++)
                    {
                        Neuron neuron = previousLayer.Neurons.ElementAt(k);
                        double weight = neuron.Weights.ElementAt(j);

                        error = weight * CalculateDelta(neuron.Output, neuron.Error);
                        currentNeuron.Error = error;
                        TrainNeuron(currentNeuron);
                    }
                }
            }
        }

        private void TrainNeuron(Neuron learner)
        {
            double delta = CalculateDelta(learner.Output, learner.Error);

            for (int i = 0; i < learner.Weights.Count(); i++)
            {
                double weight = learner.Weights.ElementAt(i);
                double input = learner.Inputs.ElementAt(i);

                weight -= input * delta * _learningRate;
                learner.ChangeWeight(value: weight, byIndex: i);
            }
        }

        private double CalculateDelta(double output, double error)
        {
            return error * CalculateSigmoidDx(output);
        }

        private double CalculateSigmoidDx(double x)
        {
            double sigmoid = 1.0 / (1.0 + Math.Exp(-x));
            return sigmoid * (1.0 - sigmoid);
        }
    }
}