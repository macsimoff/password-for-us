using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PasswordForUs.Abstractions;
using pw4us.AppConfig.Options;
using pw4us.Infrastructure.Settings;
using pw4us.Rendering;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Extensions;
using pw4us.Resources;
using pw4us.Attributes;

namespace pw4us.Commands;

public class ImportCommand(
    ILogger<ImportCommand> logger,
    IOptions<ShowSettings> showOptions,
    IEncryptionService encryptionService,
    ISaveDataController saveController,
    IImportService importService) : AsyncCommand<ImportCommand.Settings>
{
    public class Settings : PassCommandSettings
    {
        [CommandArgument(0, "<PATH>")]
        [LocalizedDescription(nameof(DescriptionResources.IC_Path))]
        public string Path { get; set; } = string.Empty;

        [CommandOption("-f|--format <FORMAT>")]
        [LocalizedDescription(nameof(DescriptionResources.IC_Format))]
        public string? Format { get; set; }
    }

    public override ValidationResult Validate(CommandContext context, Settings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Path))
            return ValidationResult.Error("File path is required.");
        if (!File.Exists(settings.Path))
            return ValidationResult.Error($"File '{settings.Path}' not found.");

        return base.Validate(context, settings);
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        logger.LogDebug("Import start. File: {Path}; Format: {Format}", settings.Path, settings.Format);

        var icon = AnsiConsoleHelpers.GetFloppyEmoji();
        AnsiConsole.Markup($"[yellow]{icon} Import new entry :[/] ");
        var spinnerAnimation = AnsiConsoleHelpers.GetSpinnerAnimation();

        var nodes = await importService
            .ImportAsync(settings.Path, settings.Format, settings.PassBytes, saveController, encryptionService)
            .Spinner(
                spinnerAnimation,
                new Style(foreground: Color.Blue));

        AnsiConsole.MarkupLine("[green]done[/]");

        if (nodes.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No entries found to import.[/]");
            return 0;
        }

        AnsiConsole.MarkupLine($"[green]Imported[/] {nodes.Count} entries.");
        NodeRenderer.Render("[blue]IMPORT RESULT[/]", showOptions.Value, nodes.Take(10));
        return 0;
    }
}