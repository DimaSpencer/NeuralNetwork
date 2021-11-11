using CsvHelper;
using System.Globalization;

namespace NeuralNetwork.Core
{
    public class CsvFileReader : IDataReader
    {
        public IList<TRow> Read<TRow>(string source)
        {
            if (source.Contains(".csv") == false)
                throw new FormatException($"Incorrect file format {source}");
            if (File.Exists(source) == false)
                throw new FileNotFoundException(nameof(source));

            using var reader = new StreamReader(source);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

            return csvReader.GetRecords<TRow>().ToList();
        }
    }
}
