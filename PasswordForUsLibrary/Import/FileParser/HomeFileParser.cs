using PasswordForUs.Abstractions.Models;
using PasswordForUsLibrary.Import.FileReader;
using PasswordForUsLibrary.Import.StringParser;

namespace PasswordForUsLibrary.Import.FileParser;

public class HomeFileParser(IFileReader fileReader, IStringParser stringParser) : IFileParser
{
    public async IAsyncEnumerable<NodeData> ParseNodesAsync(StreamReader streamReader)
    {
        await foreach (var line in fileReader.ReadAsync(streamReader))
        {
            yield return stringParser.CreateNodeData(line);
        }
    }
}