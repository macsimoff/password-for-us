namespace PasswordForUsLibrary.Import.FileReader;

public interface IFileReader
{
    IAsyncEnumerable<string> ReadAsync(StreamReader reader);
}