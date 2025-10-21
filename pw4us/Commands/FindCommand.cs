using Microsoft.Extensions.Logging;
using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;
using pw4us.Attributes;
using pw4us.Infrastructure;
using pw4us.Resources;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Extensions;

namespace pw4us.Commands;

public class FindCommand(
    ILogger<FindCommand> logger,
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
        
        AnsiConsole.Live(table)
            .Start(ctx => 
            {
                table.AddColumn(new TableColumn($"[yellow]Key[/]"));
                ctx.Refresh();
                Thread.Sleep(300);

                table.AddColumn($"[yellow]Name[/]");
                ctx.Refresh();
                Thread.Sleep(300);
                
                table.AddColumn($"[yellow]URL[/]");
                ctx.Refresh();
                Thread.Sleep(300);

                foreach (var node in data)
                {
                    table.AddRow(node.Id.ToString(), node.Title, node.Url);
                    ctx.Refresh();
                    Thread.Sleep(300);
                }
            });
    }
}