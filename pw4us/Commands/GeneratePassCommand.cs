using Microsoft.Extensions.Logging;
using PasswordForUs.Abstractions;
using pw4us.Attributes;
using pw4us.Infrastructure;
using pw4us.Resources;
using Spectre.Console;
using Spectre.Console.Cli;
namespace pw4us.Commands;

public class GeneratePassCommand(IPassGenerator generator, ILogger<GeneratePassCommand> logger) 
    : AsyncCommand<GeneratePassCommand.Settings>
{
    public class Settings : LogCommandSettings
    {
        [CommandOption("-l|--length <LENGTH>")]
        [LocalizedDescription(nameof(DescriptionResources.GPC_Length))]
        public int Length { get; set; } = 12;
    }

    public override Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        logger.LogDebug("Generating password with length {Length}", settings.Length);
        var password = generator.Generate(settings.Length, null);
        //todo: add character sets from settings
        
        var icon = AnsiConsole.Profile.Capabilities.Unicode ? "🔑 " : "> ";
        AnsiConsole.MarkupLine($"[yellow]{StringsResourse.GPC_GeneratePass}[/]");
        AnsiConsole.MarkupLine($"{icon}[gray]{password}[/]");
        return Task.FromResult(0);
    }
}