using PasswordForUsLibrary.Model;

namespace PasswordForUsLibrary.Import.FileParser;

public interface IFileParser
{
    IEnumerable<NodeDataModel> ParseNodes(StreamReader streamReader);
}