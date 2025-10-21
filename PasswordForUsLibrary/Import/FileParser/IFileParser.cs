using PasswordForUs.Abstractions.Models;

namespace PasswordForUsLibrary.Import.FileParser;

public interface IFileParser
{
    IEnumerable<NodeData> ParseNodes(StreamReader streamReader);
}