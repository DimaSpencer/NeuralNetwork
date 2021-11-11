namespace NeuralNetworkLib.Core
{
    public static class FileLoader
    {
        public static async Task SaveAsync(string filePath, string data)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException(nameof(filePath));
            if (string.IsNullOrEmpty(data))
                throw new ArgumentException(nameof(data));

            using FileStream fileStream = new(filePath, FileMode.OpenOrCreate);
            using StreamWriter writer = new(fileStream);
            await writer.WriteAsync(data);
        }

        public static async Task<string> LoadAsync(string filePath)
        {
            if (File.Exists(filePath) == false)
                throw new FileNotFoundException(nameof(filePath));

            using FileStream fileStream = new(filePath, FileMode.Open);
            using StreamReader reader = new(fileStream);
            
            return await reader.ReadToEndAsync();
        }
    }
}
