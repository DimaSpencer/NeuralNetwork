using System.Drawing;

namespace NeuralNetworkLib.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] GetColumn<T>(this T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }

        public static Bitmap ToBWImage(this double[,] pixels, double boundary = 128)
        {
            var image = new Bitmap(pixels.GetLength(1), pixels.GetLength(0));
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color color = pixels[y, x] > boundary ? Color.White : Color.Black;

                    image.SetPixel(x, y, color);
                }
            }

            return image;
        }

        public static double[,] GetAreaByIndex(this double[,] source, Point index3D, int width, int height)
        {
            double[,] matrix = new double[height, width];

            int startPointRow = index3D.Y;
            int endPointRow = index3D.Y + height;

            int startPointColumn = index3D.X;
            int endPointColumn = index3D.X + width;

            for (int rowIndex = startPointRow, matrixRow = 0; rowIndex < endPointRow; rowIndex++, matrixRow++)
            {
                for (int columnIndex = startPointColumn, matrixColumn = 0; columnIndex < endPointColumn; columnIndex++, matrixColumn++)
                {
                    matrix[matrixRow, matrixColumn] = source[rowIndex, columnIndex];
                }
            }

            return matrix;
        }
    }
}
