using PasswordForUs.Abstractions;
using pw4us.Attributes;
using pw4us.Resources;
using Spectre.Console;
using Spectre.Console.Cli;
namespace pw4us.Commands;

public class GeneratePassCommand(IPassGenerator generator) : AsyncCommand<GeneratePassCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandOption("-l|--length <LENGTH>")]
        [LocalizedDescription(nameof(DescriptionResources.GPC_Length))]
        public int Length { get; set; } = 12;
    }

    public override Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var password = generator.Generate(settings.Length, null);
        //todo: add character sets from settings
        
        AnsiConsole.MarkupLine($"[yellow]{StringsResourse.GPC_GeneratePass}[/]");
        AnsiConsole.MarkupLine($"    [gray]{password}[/]");
        return Task.FromResult(0);
    }
}