using Serilog.Core;
using Spectre.Console.Cli;

namespace pw4us.Infrastructure;

public class LogInterceptor : ICommandInterceptor
{
    public static readonly LoggingLevelSwitch LogLevel = new();

    public void Intercept(CommandContext context, CommandSettings settings)
    {
        var baseDir = AppContext.BaseDirectory;
        var logsDir = Path.Combine(baseDir, "log");

        if (settings is LogCommandSettings logSettings)
        {
            var targetPath = Path.Combine(logsDir, $"{context.Name}.log");

            if (!string.IsNullOrWhiteSpace(logSettings.LogFile))
            {
                if (Path.IsPathRooted(logSettings.LogFile))
                {
                    targetPath = logSettings.LogFile;
                }
                else
                {
                    var fileNameOnly = Path.GetFileName(logSettings.LogFile);
                    targetPath = Path.Combine(logsDir, fileNameOnly);
                }
            }

            var targetDir = Path.GetDirectoryName(targetPath);
            if (!string.IsNullOrEmpty(targetDir)) Directory.CreateDirectory(targetDir);

            LoggingEnricher.Path = targetPath;

           LogLevel.MinimumLevel = logSettings.LogLevel;
        }
        else
        {
            Directory.CreateDirectory(logsDir);
            LoggingEnricher.Path = Path.Combine(logsDir, "application.log");
            LogLevel.MinimumLevel = Serilog.Events.LogEventLevel.Information;
        }
    }
}