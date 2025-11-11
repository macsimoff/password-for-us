using PasswordForUs.Abstractions.Models;
using PasswordForUsLibrary.Import.FileReader;

namespace PasswordForUsLibrary.Import.FileParser;

public class CsvFileParser(IFileReader fileReader, GoogleCsvStringParser stringParser): IFileParser
{
    public async IAsyncEnumerable<NodeData> ParseNodesAsync(StreamReader streamReader)
    {
        var first = true;
        await foreach (var line in fileReader.ReadAsync(streamReader))
        {
            if (first)
            {
                first = false; // Skip header line
                continue;
            }
            
            yield return stringParser.CreateNodeData(line);
        }
    }
}