using PasswordForUs.Abstractions.Models;
using pw4us.AppConfig.Options;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace pw4us.Rendering;

public static class NodeRenderer
{
    private const int AnimationDelayMs = 200;

    public static void Render(string? title, ShowSettings showSettings, NodeData node)
    {
        Render(title, showSettings, [node]);
    }
    public static void Render(string? title,ShowSettings showSettings, IEnumerable<NodeData> nodeList)
    {
        var columns = GetColumns(showSettings);
        if (!columns.Any(x => x.Enabled))
        {
            AnsiConsole.MarkupLine("[yellow]No visible columns are enabled in settings.[/]");
            return;
        }
        
        var table = new Table
        {
            Border = TableBorder.Horizontal,
            ShowHeaders = true,
            Title = !string.IsNullOrEmpty(title) ? new TableTitle(title): null
        };
        AnsiConsole.WriteLine();
        AnsiConsole.Live(table).Start(ctx =>
        {
            CreateHeaders(table, ctx, columns);

            foreach (var node in nodeList)
            {
                var rowData = columns
                    .Where(c => c.Enabled)
                    .Select(c => c.Selector(node))
                    .ToArray();

                table.AddRow(rowData);
                ctx.Refresh();
                Thread.Sleep(AnimationDelayMs);
            }
        });
        AnsiConsole.WriteLine();
    }
    
    private static void CreateHeaders(Table table, LiveDisplayContext ctx, IReadOnlyList<ColumnDef> columns)
    {
        foreach (var col in columns)
        {
            if (!col.Enabled) continue;

            table.AddColumn(new TableColumn($"[yellow]{col.Header}[/]"));
            ctx.Refresh();
            Thread.Sleep(AnimationDelayMs);
        }
    }
    
    private static IReadOnlyList<ColumnDef> GetColumns(ShowSettings show)
    {
        return new List<ColumnDef>
        {
            new(enabled: show.Id, header: "Key", selector: n => new Markup(n.Id >= 0?n.Id.ToString():"<new>")),
            new(enabled: show.Name, header: "Name", 
                selector: n =>
                {
                    var name = !string.IsNullOrEmpty(n.Name) ? Markup.Escape(n.Name) : string.Empty;
                    return new Markup(name);
                }),
            new(enabled: show.Url, header: "URL", 
                selector: n =>
                {
                    var url = !string.IsNullOrEmpty(n.Url) ? Markup.Escape(n.Url) : string.Empty;
                    return new Markup(url);
                }),
            new(enabled: show.Login, header: "Login", 
                selector: n =>
                {
                    var login =  !string.IsNullOrEmpty(n.Login) ? Markup.Escape(n.Login) : string.Empty;
                    return new Markup(login);
                }),
            new(enabled: show.Password,
                header: "Password",
                selector: n =>
                {
                    var pass = !string.IsNullOrEmpty(n.Password) ? Markup.Escape(n.Password) : string.Empty;
                    return new Markup($"[gray]{pass}[/]");
                }
                ),
            new(enabled: show.User, header: "User", 
                selector: n =>
                {
                    var user = !string.IsNullOrEmpty(n.User) ? Markup.Escape(n.User) : string.Empty;
                    return new Markup(user);
                }),
            new(enabled: (show.AllData || show.DataNames.Length > 0),
                header: "",
                selector: n => GetDataTable(n.Data, show.AllData ? [] : show.DataNames))
        };
    }
    
    private sealed class ColumnDef(bool enabled, string header, Func<NodeData, IRenderable> selector)
    {
        public bool Enabled { get; } = enabled;
        public string Header { get; } = header;
        public Func<NodeData, IRenderable> Selector { get; } = selector;
    }
    
    private static IRenderable GetDataTable(Dictionary<string, string> data, params string[] columns)
    {
        if (data.Count == 0) return new Text("<empty>");

        var grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();


        KeyValuePair<string, string>[] items;

        items = columns.Length != 0
            ? columns.Select(k => k.Trim())
                .Where(data.ContainsKey)
                .Select(x => new KeyValuePair<string, string>(x, data[x]))
                .ToArray()
            : data.ToArray();

        foreach (var pair in items) grid.AddRow(Markup.Escape(pair.Key), Markup.Escape(pair.Value));
        return grid;
    }
}