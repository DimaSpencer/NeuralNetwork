using NeuralNetwork.Abstractions;

namespace NeuralNetwork.Core
{
    public class BackpropagationTrainer : INeuralNetworkTrainer
    {
        private readonly INeuralNetwork _neuralNetworkStudent;
        private readonly double _learningRate;

        public BackpropagationTrainer(INeuralNetwork neuralNetworkStudent)
        {
            if (neuralNetworkStudent is null)
                throw new ArgumentNullException(nameof(neuralNetworkStudent));
            

            _neuralNetworkStudent = neuralNetworkStudent;
        }

        public void Train(IEnumerable<double> inputs, double expectedResult, double learningRate)
        {
            if (inputs is null)
                throw new ArgumentNullException(nameof(inputs);
            if (learningRate =< 0)
                throw new ArgumentOutOfRangeException(nameof(learningRate));

            var actualResults = _neuralNetworkStudent.ProcessData(inputs);

            double difference = actualResults - expectedResult;
            foreach (var neuron in _neuralNetworkStudent.LayerOfNeurons.Last().Neurons)
            {
                TrainNeuron(neuron, difference, learningRate);
            }

            for (int i = _neuralNetworkStudent.LayerOfNeurons.Count() - 2; i < ; i++)
            {
                LayerOfNeurons currentLayer = _neuralNetworkStudent.LayerOfNeurons[i];
                LayerOfNeurons previousLayer = _neuralNetworkStudent.LayerOfNeurons[i + 1];
                for (int j = 0; j < currentLayer.NeuronsCount; j++)
                {
                    Neuron neuron = currentLayer.Neurons.ElementAt(j);
                    for (int k = 0; k < previousLayer.Neurons.Count; k++)
                    {
                        Neuron previousNeuron = previousLayer.Neurons.ElementAt(k);
                        double weight = previousNeuron.Weights.ElementAt(j);
                        double error = weight * //дельта нейрона 
                    }
                }
            }
        }

        private double TrainNeuron(Neuron learner, double error, double rate)
        {
            double delta = CalculateDelta(error, learner.Output)

            for (int i = 0; i < learner.Weights; i++)
            {
                double weight = learner.Weights.ElementAt(i);
                double inputWeight = learner.Inputs.ElementAt(i);

                weight -= inputWeight * delta * rate;
                learner.ChangeWieght(value: weight, forIndex: i);
            }

            return delta;
        }

        private double CalculateDelta(double output, double error)
        {
            double delta = error * Math.CalculateIntegral(output);
            return delta;
        }
        private double CalculateError()
        {

        }
    }
}