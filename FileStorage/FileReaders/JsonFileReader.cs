using System.Text.Json;

namespace FileStorage.FileReaders;

public class JsonFileReader(string fileName): IFileReader
{
    public async Task<Data> ReadFileAsync()
    {
        using var stream = new StreamReader(fileName);
        var fileContent = await stream.ReadToEndAsync();
        var data = JsonSerializer.Deserialize<Data>(fileContent);
        return data ?? throw new ArgumentException($"Problem with file {fileName}. Content is not valid.");
    }
}