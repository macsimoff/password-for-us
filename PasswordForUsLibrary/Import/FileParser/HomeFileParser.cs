using PasswordForUsLibrary.Import.FileReader;
using PasswordForUsLibrary.Import.StringParser;
using PasswordForUsLibrary.Model;

namespace PasswordForUsLibrary.Import.FileParser;

public class HomeFileParser(IFileReader fileReader, IStringParser stringParser) : IFileParser
{
    public IEnumerable<NodeDataModel> ParseNodes(StreamReader streamReader)
    {
        return fileReader.Read(streamReader).Select(stringParser.CreateNodeData).ToList();
    }
}