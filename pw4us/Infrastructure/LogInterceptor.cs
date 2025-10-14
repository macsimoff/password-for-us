using Microsoft.Extensions.Configuration;
using Serilog.Core;
using Serilog.Events;
using Spectre.Console.Cli;

namespace pw4us.Infrastructure;

public class LogInterceptor : ICommandInterceptor
{
    public static readonly LoggingLevelSwitch LogLevel = new();

    public LogInterceptor(IConfiguration configuration)
    {
        var levelString = configuration["Serilog:MinimumLevel:Default"];
        if (!string.IsNullOrWhiteSpace(levelString) && TryParseLevel(levelString, out var level))
            LogLevel.MinimumLevel = level;
    }

    private static bool TryParseLevel(string s, out LogEventLevel level)
    {
        var lookup = new Dictionary<string, LogEventLevel>(StringComparer.OrdinalIgnoreCase)
        {
            { "Debug", LogEventLevel.Debug },
            { "Verbose", LogEventLevel.Verbose },
            { "Information", LogEventLevel.Information },
            { "Warning", LogEventLevel.Warning },
            { "Error", LogEventLevel.Error },
            { "Fatal", LogEventLevel.Fatal }
        };
        return lookup.TryGetValue(s, out level);
    }

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
            LogLevel.MinimumLevel = logSettings.LogLevel ?? LogLevel.MinimumLevel;
        }
        else
        {
            Directory.CreateDirectory(logsDir);
            LoggingEnricher.Path = Path.Combine(logsDir, "application.log");
            LogLevel.MinimumLevel = LogEventLevel.Debug;
        }
    }
}