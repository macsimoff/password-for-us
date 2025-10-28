using Microsoft.Extensions.Options;
using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;
using pw4us.AppConfig.Options;
using pw4us.Infrastructure;
using pw4us.Rendering;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Extensions;

namespace pw4us.Commands;

public class CreateCommand(ISaveDataController controller, IOptions<ShowSettings> options): AsyncCommand<CreateCommand.Settings>
{
    public class Settings :LogCommandSettings
    {
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var icon = AnsiConsoleHelpers.GetFloppyEmoji();
        AnsiConsole.Markup($"[yellow]{icon} Save new entry :[/] ");
        var node = CreateNode(settings);
        var spinnerAnimation = AnsiConsoleHelpers.GetSpinnerAnimation();
        await controller
            .AddNewDataAsync(node)
            .Spinner(
                spinnerAnimation,
                new Style(foreground: Color.Blue));
        AnsiConsole.Markup("[green]done[/]");
        
        NodeRenderer.Render("[blue]CREATE RESULT[/]",options.Value, node);
        
        return 0;
    }

    private NodeData CreateNode(Settings settings)
    {
        return new NodeData()
        {

        };
    }
}