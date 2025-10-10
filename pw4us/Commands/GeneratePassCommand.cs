using System.ComponentModel;
using Spectre.Console.Cli;
namespace pw4us.Commands;

public class GeneratePassCommand: AsyncCommand<GeneratePassCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandOption("-l|--length <LENGTH>")]
        [Description("Length of the password to generate")]
        public int Length { get; set; } = 0;
    }

    public override Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        Console.WriteLine($"Generating password of length: {settings.Length}");
        return Task.FromResult(0);
    }
}