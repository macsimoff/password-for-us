using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PasswordForUs.Abstractions;
using pw4us.AppConfig.Options;
using pw4us.Attributes;
using pw4us.Infrastructure;
using pw4us.Rendering;
using pw4us.Resources;
using Spectre.Console;
using Spectre.Console.Cli;

namespace pw4us.Commands;

public class GeneratePassCommand(IPassGenerator generator, 
    ILogger<GeneratePassCommand> logger, IOptions<GeneratePassSettings> options) 
    : AsyncCommand<GeneratePassCommand.Settings>
{
    public class Settings : LogCommandSettings
    {
        [CommandOption("-l|--length <LENGTH>")]
        [LocalizedDescription(nameof(DescriptionResources.GPC_Length))]
        public int Length { get; set; }
    }

    public override Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        logger.LogDebug("Generating password with length {Length}", settings.Length);
        
        try
        {
            var passLength = settings.Length > 0 ? 
                settings.Length : options.Value.Length;
            var alphabet = options.Value.Alphabet;
            var password = Markup.Escape(generator.Generate(passLength, alphabet));

            var icon = AnsiConsoleHelpers.GetKeyEmoji();
            AnsiConsole.MarkupLine($"[yellow]{StringsResourse.GPC_GeneratePass}[/]");
            AnsiConsole.MarkupLine($"{icon} [gray]{password}[/]");
            return Task.FromResult(0);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid password options: {Message}", ex.Message);
            AnsiConsole.MarkupLine($"[red]{StringsResourse.Error}:[/] [gray]{StringsResourse.GPC_ArgumentExceptionMessage}[/]");
            return Task.FromResult(1);
        }
    }
}