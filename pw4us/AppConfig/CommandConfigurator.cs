using System.Globalization;
using pw4us.Commands;
using pw4us.Resources;
using Spectre.Console.Cli;

namespace pw4us.AppConfig;

public abstract class CommandConfigurator
{
    public static void Configure(IConfigurator config)
    {
        config.SetApplicationName("pw4us");
        config.SetApplicationCulture(new CultureInfo("ru"));
#if DEBUG
        config.PropagateExceptions();
        config.ValidateExamples();
#endif
        config.AddCommand<GeneratePassCommand>("genpass")
            .WithAlias("gp")
            .WithDescription(DescriptionResources.GeneratePassCommand)
            .WithExample(new[] { "genpass", "--length", "16" });
        // Add more commands here as needed
    }
}