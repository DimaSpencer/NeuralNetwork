using NeuralNetworkLib.Abstractions;
using NeuralNetworkLib.Maths;

namespace NeuralNetworkLib.Core
{
    public class BackpropagationTrainer : INeuralNetworkTrainer
    {
        private readonly INeuralNetwork _neuralNetworkStudent;
        private double _learningRate;

        public BackpropagationTrainer(INeuralNetwork neuralNetworkStudent, double learningRate = 0.1)
        {
            if (neuralNetworkStudent is null)
                throw new ArgumentNullException(nameof(neuralNetworkStudent));
            if (learningRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(learningRate));

            _neuralNetworkStudent = neuralNetworkStudent;
            _learningRate = learningRate;
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

        public void StudyingAtDataset(Dataset dataset, int epoch, CancellationToken? token = null)
        {
            if (dataset is null)
                throw new ArgumentNullException(nameof(dataset));
            if (epoch < 0)
                throw new ArgumentOutOfRangeException(nameof(epoch));

            for (int i = 0; i < epoch && (token?.IsCancellationRequested ?? true); i++)
            {
                //double error = 0.0;
                for (int j = 0; j < dataset.Sets.Count; j++)
                {
                    var inputs = dataset.Sets.ElementAt(j).Key;
                    var expectedResults = dataset.Sets.ElementAt(j).Value;

                    Task.Run(() => TrainNetwork(inputs, expectedResults));
                }

                //if (i % 100 == 0)
                    Console.WriteLine($"{i + 1} эпоха пройдена");
            }
        }

        private double TrainNetwork(IEnumerable<double> inputs, IEnumerable<double> expectedResults)
        {
            if (inputs is null)
                throw new ArgumentNullException(nameof(inputs));
            if(_neuralNetworkStudent.LayerOfNeurons.First().NeuronsCount != inputs.Count())
                throw new ArgumentOutOfRangeException(nameof(inputs));
            if (_neuralNetworkStudent.LayerOfNeurons.Last().NeuronsCount != expectedResults.Count())
                throw new ArgumentOutOfRangeException(nameof(inputs));

            double resultError = 0.0;
            var actualResults = _neuralNetworkStudent.ProcessData(inputs);

            //для последнего результирующего слоя
            var lastNeurons = _neuralNetworkStudent.LayerOfNeurons.Last().Neurons;
            for (int i = 0; i < expectedResults.Count(); i++)
            {
                Neuron currentNeuron = lastNeurons.ElementAt(i);

                double error = actualResults.ElementAt(i) - expectedResults.ElementAt(i);
                currentNeuron.Error = error;
                resultError += error;

                TrainNeuron(currentNeuron);
            }

            resultError /= expectedResults.Count(); //вычисляем среднюю ошибку
            //для остальных нейронов
            for (int i = _neuralNetworkStudent.LayerOfNeurons.Count - 2; i >= 0; i--)
            {
                LayerOfNeurons currentLayer = _neuralNetworkStudent.LayerOfNeurons.ElementAt(i);
                LayerOfNeurons previousLayer = _neuralNetworkStudent.LayerOfNeurons.ElementAt(i + 1);

                for (int j = 0; j < currentLayer.NeuronsCount; j++)
                {
                    Neuron currentNeuron = currentLayer.Neurons.ElementAt(j);

                    for (int k = 0; k < previousLayer.Neurons.Count; k++)
                    {
                        Neuron neuron = previousLayer.Neurons.ElementAt(k);
                        double weight = neuron.Weights.ElementAt(j);

                        currentNeuron.Error = weight * CalculateDelta(neuron.Output, neuron.Error);
                        TrainNeuron(currentNeuron);
                    }
                }
            }

            resultError = Math.Pow(resultError, 2);
            return resultError;
        }

        private void TrainNeuron(Neuron learner)
        {
            double delta = CalculateDelta(learner.Output, learner.Error);

            for (int i = 0; i < learner.Weights.Count; i++)
            {
                double weight = learner.Weights.ElementAt(i);
                double input = learner.Inputs.ElementAt(i);

                weight -= input * delta * _learningRate;
                learner.ChangeWeight(value: weight, byIndex: i);
            }
        }

        private double CalculateDelta(double output, double error)
        {
            double dx = output * (1.0 - output);
            return error * dx;
        }
    }
}