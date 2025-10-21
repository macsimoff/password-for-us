using System.ComponentModel;
using Microsoft.Extensions.Logging;
using pw4us.Attributes;
using pw4us.Infrastructure;
using pw4us.Resources;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Extensions;
using Spectre.Console.Rendering;

namespace pw4us.Commands;

public class FindCommand(ILogger<FindCommand> logger): AsyncCommand<FindCommand.Settings>
{
    public class Settings : LogCommandSettings
    {
        [CommandOption("-i|--id <ID>")]
        [LocalizedDescription(nameof(DescriptionResources.FC_Id))]
        [DefaultValue(-1)]
        public int Id { get; set; }
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
        
        await Task.Delay(2000)
            .Spinner(
                spinnerAnimation,
                new Style(foreground: Color.Blue));
        
        DrowTable();

        return await Task.FromResult(0);
    }

    private Spinner GetSpinnerAnimation()
    {
        return AnsiConsole.Profile.Capabilities.Unicode ? Spinner.Known.Clock: Spinner.Known.Binary;
    }

    private void DrowTable()
    {
        var table = new Table
        {
            Border = TableBorder.Horizontal,
            ShowHeaders = true
        };
        
        AnsiConsole.Live(table)
            .Start(ctx => 
            {
                table.AddColumn(new TableColumn($"[yellow]Bar[/]"));
                ctx.Refresh();
                Thread.Sleep(300);

                table.AddColumn($"[yellow]Bar[/]");
                ctx.Refresh();
                Thread.Sleep(300);
            });
    }
}