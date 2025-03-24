using PasswordForUsLibrary.Model;

namespace PasswordForUsLibrary.Import.StringParser;

public class HomeFileStringParser: IStringParser
{
    private const string UserTag = "passuser>>";
    private const string NameTag = "name>>";
    private const string UrlTag = "url>>";
    private const string LoginTag = "login>>";
    private const string PasswordTag = "password>>";
    public const string TagSeparator = ">>";
    public const char Delimiter = '\n';

    public NodeDataModel CreateNodeData(string nodeString)
    {
        string?[] lines = nodeString.Split(Delimiter);
        var user = GetUser(lines);
        var name = GetName(lines);
        var url = GetUrl(lines);
        var login = GetLogin(lines);
        var password = GetPassword(lines);
        var node = new NodeDataModel(user,name, url, login, password);
        node.Data = GetData(lines);
        return node;
    }

    private Dictionary<string, string> GetData(string?[] lines)
    {
        var res = new Dictionary<string, string>();
        foreach (var line in lines)
        {
            if (line.Contains(TagSeparator) && !line.ToLower().Contains(NameTag)
                                            && !line.ToLower().Contains(UrlTag)
                                            && !line.ToLower().Contains(LoginTag)
                                            && !line.ToLower().Contains(PasswordTag)
                                            && !line.ToLower().Contains(UserTag))
            {
                var s = line.Split(TagSeparator);
                var tagName = s[0];
                var tagValue = s[1];
                res.Add(tagName.Trim(), tagValue.Trim());
            }
        }

        return res;
    }

    private string? GetUser(string?[] lines)
    {
        return GetTagValue(lines, UserTag);
    }

    private string? GetName(string?[] lines)
    {
        var name = GetTagValue(lines, NameTag);
        if (!string.IsNullOrEmpty(name)) return name;

        var name2 = GetSimpleLines(lines).FirstOrDefault();
        return !string.IsNullOrEmpty(name2) ? name2 : GetUrl(lines);
    }

    private string? GetUrl(string?[] lines)
    {
        var url = GetTagValue(lines, UrlTag);
        if (!string.IsNullOrEmpty(url)) return url;
        
        var res = lines.FirstOrDefault(x => x.StartsWith("http"));
        var simpleLines = GetSimpleLines(lines);
        
        return !string.IsNullOrEmpty(res) ? res : simpleLines.Count == 4 ? lines[1] : "";
    }

    private string? GetLogin(string?[] lines)
    {
        var login = GetTagValue(lines,LoginTag);
        if (!string.IsNullOrEmpty(login)) return login;
        
        var simpleLines = GetSimpleLines(lines);
        return simpleLines.Count switch
        {
            3 => simpleLines[1],
            4 => simpleLines[2],
            _ => ""
        };
    }

    private string? GetPassword(string?[] lines)
    {
        var pass = GetTagValue(lines, PasswordTag);
        if (!string.IsNullOrEmpty(pass)) return pass;

        var simpleLines = GetSimpleLines(lines);
        return simpleLines.Count switch
        {
            2 => simpleLines[1],
            3 => simpleLines[2],
            4 => simpleLines[3],
            _ => ""
        };
    }

    private string? GetTagValue(string?[] lines, string nameTag)
    {
        var value = lines.FirstOrDefault(x => x.ToLower().StartsWith(nameTag));
        return !string.IsNullOrEmpty(value) ? value[nameTag.Length..].Trim(): string.Empty;
    }

    private static List<string?> GetSimpleLines(string?[] lines)
    {
        return lines.Where(x => !x.Contains(TagSeparator)).ToList();
    }
}