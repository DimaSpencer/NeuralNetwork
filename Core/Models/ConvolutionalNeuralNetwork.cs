
namespace NeuralNetwork.Core.Models
{
    public class ConvolutionalNeuralNetwork
    {
        private List<NeuronGroup> _neuronGroups;
        
        public virtual void ProcessData(double[,] image)
        {
            if (image is null)
                throw new ArgumentNullException(nameof(image), "Image is null");

            double[,] filter = { { 0, 0, 0 }, { 1, 1, 1 }, { 0, 0, 0} };
            double[,] filter2 = { { 1, 1, 1 }, { 1, 1, 1 }, { 0, 0, 0} };

            NeuronGroup group = new(filter);

            group.Process(image, 1);
        }
        
        private double[,] ConnectFilterToArea(double[,] area, double[,] filter)
        {
            int rowCount = filter.GetLength(0);
            int coulumnCount = filter.GetLength(1);

            if (area.GetLength(0) != filter.GetLength(0))
                throw new ArgumentOutOfRangeException("Length of one of the parameters is out of range");
            if (area.GetLength(1) != filter.GetLength(1))
                throw new ArgumentOutOfRangeException("Length of one of the parameters is out of range");

            double[,] result = new double[rowCount, coulumnCount];

            for (int column = 0; column < coulumnCount; column++)
            {
                for (int row = 0; row < rowCount; row++)
                {

                }
            }

            return result;
        }

        //private double[,] CalculateMaxPooling(double[,] matrix)
        //{

        //}
    }
}
