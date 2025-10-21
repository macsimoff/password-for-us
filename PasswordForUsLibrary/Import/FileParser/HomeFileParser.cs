using PasswordForUs.Abstractions.Models;
using PasswordForUsLibrary.Import.FileReader;
using PasswordForUsLibrary.Import.StringParser;

namespace PasswordForUsLibrary.Import.FileParser;

public class HomeFileParser(IFileReader fileReader, IStringParser stringParser) : IFileParser
{
    public IEnumerable<NodeData> ParseNodes(StreamReader streamReader)
    {
        return fileReader.Read(streamReader)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(stringParser.CreateNodeData);
    }
}