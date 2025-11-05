namespace FileStorage.FileReaders;

public interface IWriter
{
    Task Write(Data data);
}