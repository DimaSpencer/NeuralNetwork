using System.Collections.Generic;
using System.Drawing;

namespace NeuralNetwork.Core
{
    public class PictureConverter
    {
        private int _resizeWidth = 20;
        private int _resizeHeight = 20;

        public int Boundary { get; set; } = 128;

        public double[,] ConvertTo2dArray(string picturePath, Size size = default)
        {
            if (string.IsNullOrEmpty(picturePath))
                throw new ArgumentNullException("Picture path is null or empty", nameof(picturePath));
            if (Path.HasExtension(picturePath) == false)
                throw new FormatException("Invalid file format");
            if (File.Exists(picturePath) == false)
                throw new FileNotFoundException("File not found", picturePath);

            Bitmap image = new(picturePath);

            if(size != default)
            {
                image = new Bitmap(image, size);
            }

            double[,] result = new double[image.Height, image.Width];

            for (int row = 0; row < image.Height - 1; row++)
            {
                for (int column = 0; column < image.Width - 1; column++)
                {
                    Color pixel = image.GetPixel(column, row);
                    double value = Brightness2(pixel);

                    result[row, column] = value;
                }
            }
            return result;
        }

        //тестовый метод, для проверки изображения на корректность
        public void Save(string path, double[,] pixels)
        {
            int width = pixels.GetLength(1);
            int heigth = pixels.GetLength(0);

            var image = new Bitmap(pixels.GetLength(1), pixels.GetLength(0));
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color color = pixels[y, x] > Boundary ? Color.White : Color.Black;

                    image.SetPixel(x, y, color);
                }
            }

            image.Save(path);
        }

        private double Brightness2(Color pixel)
        {
            var result = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;
            return result;
        }

        private double Brightness(Color pixel)
        {
            var result = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;
            return result < Boundary ? 0.0 : 1.0;
        }
    }
}
