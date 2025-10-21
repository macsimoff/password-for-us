using PasswordForUs.Abstractions.Models;
using PasswordForUsLibrary.Import.FileReader;

namespace PasswordForUsLibrary.Import.FileParser;

public class CsvFileParser(IFileReader fileReader, GoogleCsvStringParser stringParser): IFileParser
{
    public IEnumerable<NodeData> ParseNodes(StreamReader streamReader)
    {
        var lines = fileReader.Read(streamReader)
            .Skip(1)                                   // Skip header line
            .Where(l => !string.IsNullOrWhiteSpace(l));

        foreach (var line in lines)
            yield return stringParser.CreateNodeData(line);
    }
}