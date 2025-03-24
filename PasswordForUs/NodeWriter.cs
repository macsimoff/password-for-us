using PasswordForUs.ConsoleExtension;
using PasswordForUs.Settings;
using PasswordForUsLibrary.Model;

namespace PasswordForUs;

public class NodeWriter
{
    public void Write(NodeDataModel[] items, ShowSettings show)
    {
        Console.Write("\n");

        var lineLength = CalculateLineLength(items)+3;
        var lineCount = Console.WindowWidth / lineLength;

        for (int i = 0; i < items.Length; i += lineCount)
        {
            var itemsSlice = items.Skip(i).Take(lineCount).ToArray();
            WriteProperties(itemsSlice, lineLength, show);

            if (i + lineCount < items.Length)
            {
                WriteRowSeparator();
            }
        }

        Console.Write("\n");
    }

    private void WriteProperties(NodeDataModel[] itemsSlice, int lineLength, ShowSettings show)
    {
        if (show.Id)
            WriteRows(itemsSlice, lineLength, item => $"id >> {item.Id}");
        if (show.Name)
            WriteRows(itemsSlice, lineLength, item => $"title >> {item.Title}");
        if (show.Url)
            WriteRows(itemsSlice, lineLength, item => $"url >> {item.Url}");
        if (show.Login)
            WriteRows(itemsSlice, lineLength, item => $"login >> {item.Login}");
        if (show.Password)
            WriteRows(itemsSlice, lineLength, item => $"password >> {item.Password}");
        if (show.User)
            WriteRows(itemsSlice, lineLength, item => $"user >> {item.User}");
        if (show.AllData || show.DataNames.Length > 0)
            WriteData(itemsSlice, lineLength, show);
    }

    private void WriteData(NodeDataModel[] itemsSlice, int lineLength, ShowSettings show)
    {
        if (show.AllData || show.DataNames.Length == 0)
        {
            var max = itemsSlice.Max(item => item.Data.Count);
            for (var j = 0; j < max; j++)
            {
                var j1 = j;
                WriteRows(itemsSlice, lineLength, item =>
                {
                    if(item.Data.Count <= j1) return "";
                    
                    var key = item.Data.Keys.ElementAt(j1);
                    return $"{key} >> {item.Data[key]}";
                });
            }
        }
        else
        {
            foreach (var dataName in show.DataNames)
            {
                WriteRows(itemsSlice, lineLength, item =>
                    item.Data.TryGetValue(dataName, out var value) ? $"{dataName} >> {value}" : "");
            }
        }
    }

    private void WriteRows(NodeDataModel[] items, int lineLength, Func<NodeDataModel, string> getRowText)
    {
        foreach (var node in items)
        {
            var s = getRowText(node);
            Console.Write(s);
            AppConsoleExtension.WriteGap(lineLength - s.Length);
        }

        Console.Write("\n");
    }

    private void WriteRowSeparator()
    {
        AppConsoleExtension.WriteRowSeparator(Console.WindowWidth);
        Console.Write("\n");
    }

    private int CalculateLineLength(NodeDataModel[] items)
    {
        var maxLength = 0;

        foreach (var node in items)
        {
            maxLength = Math.Max(maxLength, GetPropertyLength("title", node.Title));
            maxLength = Math.Max(maxLength, GetPropertyLength("url", node.Url));
            maxLength = Math.Max(maxLength, GetPropertyLength("login", node.Login));
            maxLength = Math.Max(maxLength, GetPropertyLength("password", node.Password));
            maxLength = Math.Max(maxLength, GetPropertyLength("user", node.User));

            maxLength = node.Data.Select(data => GetPropertyLength(data.Key, data.Value))
                .Prepend(maxLength)
                .Max();
        }

        return maxLength;
    }

    private int GetPropertyLength(string propertyName, string? propertyValue)
    {
        return propertyValue != null ? $"{propertyName} >> {propertyValue}".Length : 0;
    }
}