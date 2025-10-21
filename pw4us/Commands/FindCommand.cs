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

namespace pw4us.Commands;

public class FindCommand(
    ILogger<FindCommand> logger,
    IOptions<ShowSettings> options,
    ISearchDataController controller): AsyncCommand<FindCommand.Settings>
{
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
        logger.LogDebug("Executing FindCommand with Id: {Id}, Url: {Url}, Name: {Name}", 
            settings.Id, settings.Url, settings.Name);
        
        var spinnerAnimation = GetSpinnerAnimation();
        
        var data = await controller
            .SearchAsync(new SearchDataModel(settings.Id, settings.Url, settings.Name))
            .Spinner(
                spinnerAnimation,
                new Style(foreground: Color.Blue));
        
        DrowTable(data);

        return 0;
    }

    private Spinner GetSpinnerAnimation()
    {
        return AnsiConsole.Profile.Capabilities.Unicode ? Spinner.Known.Clock: Spinner.Known.Binary;
    }

    private void DrowTable(IEnumerable<NodeData> data)
    {
        var table = new Table
        {
            Border = TableBorder.Horizontal,
            ShowHeaders = true
        };

        AnsiConsole.WriteLine();
        AnsiConsole.Live(table)
            .Start(ctx => 
            {
                CreateHeaders(table, ctx);

                foreach (var node in data)
                {
                    var rowData = new List<string>();
                    if(options.Value.Id) rowData.Add(node.Id.ToString());
                    if(options.Value.Name) rowData.Add(node.Title??string.Empty);
                    if(options.Value.Url) rowData.Add(node.Url??string.Empty);
                    if(options.Value.Login) rowData.Add(node.Login??string.Empty);
                    if(options.Value.Password) rowData.Add(node.Password??string.Empty);
                    if(options.Value.User) rowData.Add(node.User??string.Empty);
                    
                    table.AddRow(rowData.ToArray());
                    ctx.Refresh();
                    Thread.Sleep(300);
                }
            });
        AnsiConsole.WriteLine();
    }

    private void CreateHeaders(Table table, LiveDisplayContext ctx)
    {
        const int delay = 300;
        table.Title = new TableTitle("[blue]SEARCH RESULT[/]");
        if(options.Value.Id)
        {
            table.AddColumn(new TableColumn($"[yellow]Key[/]"));
            ctx.Refresh();
            Thread.Sleep(delay);
        }

        if (options.Value.Name)
        {
            table.AddColumn($"[yellow]Name[/]");
            ctx.Refresh();
            Thread.Sleep(delay);
        }

        if (options.Value.Url)
        {
            table.AddColumn($"[yellow]URL[/]");
            ctx.Refresh();
            Thread.Sleep(delay);
        }

        if (options.Value.Login)
        {
            table.AddColumn(new TableColumn($"[yellow]Login[/]"));
            ctx.Refresh();
            Thread.Sleep(delay);
        }

        if (options.Value.Password)
        {
            table.AddColumn(new TableColumn($"[yellow]Password[/]"));
            ctx.Refresh();
            Thread.Sleep(delay);
        }

        if (options.Value.User)
        {
            table.AddColumn(new TableColumn($"[yellow]User[/]"));
            ctx.Refresh();
            Thread.Sleep(delay);
        }
    }
}