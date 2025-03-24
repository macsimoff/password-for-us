namespace PasswordForUsLibrary.Import.FileReader;

public interface IFileReader
{
    IEnumerable<string> Read(StreamReader reader);
}