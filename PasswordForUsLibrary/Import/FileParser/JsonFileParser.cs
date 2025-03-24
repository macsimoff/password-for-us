using System.Text.Json;
using PasswordForUsLibrary.Model;

namespace PasswordForUsLibrary.Import.FileParser;

public class JsonFileParser:IFileParser
{
    public IEnumerable<NodeDataModel> ParseNodes(StreamReader streamReader)
    {
        var jsonString = streamReader.ReadToEnd();
        var data = JsonSerializer.Deserialize<List<NodeDataModel>>(jsonString);
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