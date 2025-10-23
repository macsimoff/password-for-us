namespace FileStorage.FileReaders;

public interface IFileReader
{
    Task<Data> ReadFileAsync();
}