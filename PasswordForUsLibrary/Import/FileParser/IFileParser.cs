using PasswordForUs.Abstractions.Models;

namespace PasswordForUsLibrary.Import.FileParser;

public interface IFileParser
{
    IAsyncEnumerable<NodeData> ParseNodesAsync(StreamReader streamReader);
}