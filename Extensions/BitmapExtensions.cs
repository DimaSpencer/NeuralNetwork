using System.Drawing;

namespace NeuralNetworkLib.Extensions
{
    public static class BitmapExtensions
    {
        public static Bitmap [] GetRgbChannels(this Bitmap source)
        {
            Bitmap[] result = new Bitmap[3] { new Bitmap(source.Width, source.Height), new Bitmap(source.Width, source.Height), new Bitmap(source.Width, source.Height) };
            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    Color color = source.GetPixel(i, j);
                    byte r = color.R;
                    byte b = color.B;
                    byte g = color.G;

                    result[0].SetPixel(i, j, Color.FromArgb(color.A, color.R, 0, 0));
                    result[1].SetPixel(i, j, Color.FromArgb(color.A, 0, color.G, 0));
                    result[2].SetPixel(i, j, Color.FromArgb(color.A, 0, 0, color.B));
                }
            }
            return result;
        }

        public static IList<double[,]> ConvertToRGBMatrixList(this Bitmap source, Size newSize = default)
        {
            source = newSize == default ? source : new Bitmap(source, newSize);

            int chanalCount = 3;
            List<double[,]> result = new List<double[,]>
            {
                new double[source.Height, source.Width],
                new double[source.Height, source.Width],
                new double[source.Height, source.Width]
            };

            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    Color color = source.GetPixel(x, y);

                    result[0][y, x] = color.R;
                    result[1][y, x] = color.B;
                    result[2][y, x] = color.G;

                }
            }
            return result;
        }
    }
}
