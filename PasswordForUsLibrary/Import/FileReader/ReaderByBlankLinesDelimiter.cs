using System.Text;
using PasswordForUsLibrary.Import.StringParser;

namespace PasswordForUsLibrary.Import.FileReader;

public class LineFileReader : IFileReader
{
    public async IAsyncEnumerable<string> ReadAsync(StreamReader reader)
    {
        var builder = new StringBuilder();
        while (await reader.ReadLineAsync() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line) && builder.Length > 0)
            {
                yield return builder.ToString();
                builder.Clear();
                continue;
            }
            
            if(string.IsNullOrWhiteSpace(line)) continue;

            if(builder.Length > 0) builder.Append(HomeFileStringParser.Delimiter);
            builder.Append(line.Trim());
        }
        
        if(builder.Length > 0)
            yield return builder.ToString();
    }
}