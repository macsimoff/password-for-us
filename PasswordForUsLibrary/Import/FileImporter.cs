using PasswordForUsLibrary.DataRepository;
using PasswordForUsLibrary.Exception;
using PasswordForUsLibrary.Import.FileParser;
using PasswordForUsLibrary.Model;

namespace PasswordForUsLibrary.Import;

public class FileImporter(IFileParser fileParser, IRepository repository)
{
    public void Import(ImportData data)
    {
        if (!data.PathIsValid) throw new PathInvalidException($"File {data.Path} path is invalid.");
        if(!File.Exists(data.Path)) throw new ArgumentException($"File {data.Path} not found.");

        using var streamReader = new StreamReader(data.Path);
        var nodeList = fileParser.ParseNodes(streamReader);

        foreach (var node in nodeList)
        {
            repository.AddNode(node);
        }
    }
}