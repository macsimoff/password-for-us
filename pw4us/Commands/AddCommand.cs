using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;
using pw4us.Infrastructure;
using pw4us.Utils;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Extensions;

namespace pw4us.Commands;

public class AddCommand(ISaveDataController controller): AsyncCommand<AddCommand.Settings>
{
    public class Settings :LogCommandSettings
    {
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var icon = ConsoleUtils.GetFloppyEmoji();
        AnsiConsole.Markup($"[yellow]{icon} Save new data :[/] ");
        var node = CreateNode(settings);
        var spinnerAnimation = ConsoleUtils.GetSpinnerAnimation();
        await controller
            .AddNewDataAsync(node)
            .Spinner(
                spinnerAnimation,
                new Style(foreground: Color.Blue));
        AnsiConsole.Markup("[green]done[/]");
        return 0;
    }

    private NodeData CreateNode(Settings settings)
    {
        return new NodeData()
        {

        };
    }
}