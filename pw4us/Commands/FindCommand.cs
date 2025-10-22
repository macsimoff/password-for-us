using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;
using pw4us.AppConfig.Options;
using pw4us.Attributes;
using pw4us.Infrastructure;
using pw4us.Resources;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Extensions;
using Spectre.Console.Rendering;

namespace pw4us.Commands;

public class FindCommand(
    ILogger<FindCommand> logger,
    IOptions<ShowSettings> options,
    ISearchDataController controller) : AsyncCommand<FindCommand.Settings>
{
    private const int AnimationDelayMs = 200;

    public class Settings : LogCommandSettings
    {
        [CommandOption("-i|--id <ID>")]
        [LocalizedDescription(nameof(DescriptionResources.FC_Id))]
        public int? Id { get; set; }

        [CommandOption("-u|--url <URL>")]
        [LocalizedDescription(nameof(DescriptionResources.FC_Url))]
        public string? Url { get; set; }

        [CommandOption("-n|--name <NAME>")]
        [LocalizedDescription(nameof(DescriptionResources.FC_Name))]
        public string? Name { get; set; }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        LogCommandDebugInfo(settings);

        var spinnerAnimation = GetSpinnerAnimation();

        var data = await controller
            .SearchAsync(new SearchDataModel(settings.Id, settings.Url, settings.Name))
            .Spinner(
                spinnerAnimation,
                new Style(foreground: Color.Blue));

        DrawTable(data);

        return 0;
    }

    private void LogCommandDebugInfo(Settings settings)
    {
        logger.LogDebug("Executing FindCommand with Settings -> Id: {Id}, Url: {Url}, Name: {Name}",
            settings.Id, settings.Url, settings.Name);
        logger.LogDebug("Executing FindCommand with ShowSettings -> " +
                        "All: {All}, Id: {Id}, Name: {Name}, Url: {Url}, Login: {Login}," +
                        "User: {User}, Password: {Password}, AllData: {AllData}, Data: {DataNames}",
            options.Value.All,
            options.Value.Id,
            options.Value.Name,
            options.Value.Url,
            options.Value.Login,
            options.Value.User,
            options.Value.Password,
            options.Value.AllData,
            options.Value.DataNames.ToString());
    }

    private Spinner GetSpinnerAnimation()
    {
        return AnsiConsole.Profile.Capabilities.Unicode ? Spinner.Known.Clock : Spinner.Known.Binary;
    }

    private void DrawTable(IEnumerable<NodeData> data)
    {
        var columns = GetColumns();
        if (!columns.Any(c => c.Enabled))
        {
            AnsiConsole.MarkupLine("[yellow]No visible columns are enabled in settings.[/]");
            return;
        }

        var rows = data?.ToList() ?? new List<NodeData>();
        if (rows.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No results found.[/]");
            return;
        }

        var table = new Table
        {
            Border = TableBorder.Horizontal,
            ShowHeaders = true,
            Title = new TableTitle("[blue]SEARCH RESULT[/]")
        };

        AnsiConsole.WriteLine();
        AnsiConsole.Live(table).Start(ctx =>
        {
            CreateHeaders(table, ctx, columns);

            foreach (var node in rows)
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

    private void CreateHeaders(Table table, LiveDisplayContext ctx, IReadOnlyList<ColumnDef> columns)
    {
        foreach (var col in columns)
        {
            if (!col.Enabled) continue;

            table.AddColumn(new TableColumn($"[yellow]{col.Header}[/]"));
            ctx.Refresh();
            Thread.Sleep(AnimationDelayMs);
        }
    }

    private IReadOnlyList<ColumnDef> GetColumns()
    {
        var show = options.Value;

        return new List<ColumnDef>
        {
            new(enabled: show.Id, header: "Key", selector: n => new Markup(n.Id.ToString())),
            new(enabled: show.Name, header: "Name", selector: n => new Markup(n.Title ?? string.Empty)),
            new(enabled: show.Url, header: "URL", selector: n => new Markup(n.Url ?? string.Empty)),
            new(enabled: show.Login, header: "Login", selector: n => new Markup(n.Login ?? string.Empty)),
            new(enabled: show.Password,
                header: "Password",
                selector: n => new Markup($"[gray]{n.Password ?? string.Empty}[/]")),
            new(enabled: show.User, header: "User", selector: n => new Markup(n.User ?? string.Empty)),
            new(enabled: (show.AllData || show.DataNames.Length > 0),
                header: "",
                selector: n => GetDataTable(n.Data, show.AllData ? [] : show.DataNames))
        };
    }

    private IRenderable GetDataTable(Dictionary<string, string> data, params string[] columns)
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

        foreach (var pair in items) grid.AddRow(pair.Key, pair.Value);
        return grid;
    }

    private sealed class ColumnDef(bool enabled, string header, Func<NodeData, IRenderable> selector)
    {
        public bool Enabled { get; } = enabled;
        public string Header { get; } = header;
        public Func<NodeData, IRenderable> Selector { get; } = selector;
    }
}