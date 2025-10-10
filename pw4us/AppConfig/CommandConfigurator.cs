using pw4us.Commands;
using Spectre.Console.Cli;

namespace pw4us.AppConfig;

public abstract class CommandConfigurator
{
    public static void Configure(IConfigurator config)
    {
        config.SetApplicationName("pw4us");
#if DEBUG
        config.PropagateExceptions();
        config.ValidateExamples();
#endif
        config.AddCommand<GeneratePassCommand>("genpass")
            .WithAlias("gp")
            .WithDescription("Generate a random password")
            .WithExample(new[] { "genpass", "--length", "16" });
        // Add more commands here as needed
    }
}