using Serilog.Core;
using Spectre.Console.Cli;

namespace pw4us.Infrastructure;

public class LogInterceptor : ICommandInterceptor
{
    public static readonly LoggingLevelSwitch LogLevel = new();

    public void Intercept(CommandContext context, CommandSettings settings)
    {
        if (settings is LogCommandSettings logSettings)
        {
            LoggingEnricher.Path = string.IsNullOrWhiteSpace(logSettings.LogFile)
                ?$"{context.Name}.log"
                : logSettings.LogFile;

            var level = logSettings.LogLevel;
        }
        else
        {
            LoggingEnricher.Path = "application.log";
            LogLevel.MinimumLevel = Serilog.Events.LogEventLevel.Information;
        }
    }
}