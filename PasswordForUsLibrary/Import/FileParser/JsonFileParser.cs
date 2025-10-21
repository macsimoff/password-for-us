using System.Text.Json;
using PasswordForUs.Abstractions.Models;

namespace PasswordForUsLibrary.Import.FileParser;

public class JsonFileParser:IFileParser
{
    public IEnumerable<NodeData> ParseNodes(StreamReader streamReader)
    {
        var jsonString = streamReader.ReadToEnd();
        var data = JsonSerializer.Deserialize<List<NodeData>>(jsonString);
        data ??= [];
        foreach (var node in data)
        {
            if (node.Guid == Guid.Empty)
                node.Guid = Guid.NewGuid();
            if (node.ChangeTimeTicks == 0)
                node.ChangeTimeTicks = DateTime.Now.Ticks;
        }
        return data;
    }
}