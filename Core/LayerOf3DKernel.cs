namespace NeuralNetwork.Core.Models
{
    //public class LayerOf3DKernel
    //{
    //    private readonly List<ConvolutionKernel3D> _kernels;

    //    public LayerOf3DKernel(int kernalSize, int kernalCount)
    //    {
    //        if (kernalSize <= 0)
    //            throw new ArgumentOutOfRangeException(nameof(kernalSize), "Kernal size out of range");
    //        if (kernalSize <= 0)
    //            throw new ArgumentOutOfRangeException(nameof(kernalCount), "Kernal count out of range");

    //        _kernels = new List<ConvolutionKernel3D>(kernalCount);

    //        for (int i = 0; i < kernalCount; i++)
    //        {
    //            ConvolutionKernel2D kernal = new(new double[kernalSize, kernalSize]);
    //            kernal.InitializeRandomValues();
    //            _kernels.Add(kernal);
    //        }
    //    }

    //    public IList<double[,,]> ProcessInput<T>(double[,,] input, int padding = 0, int stride = 1)
    //    {
    //        //TODO: добавить паддинг к входному изображения
    //        List<double[,,]> activationMaps = new(_kernels.Count);
    //        foreach (var kernal in _kernels)
    //        {
    //            double[,,] activationMap = kernal.Process(input, stride);
    //            activationMaps.Add(activationMap);
    //        }

    //        return activationMaps;
    //    }
    //}
}
