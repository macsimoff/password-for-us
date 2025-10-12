using Spectre.Console;
using Spectre.Console.Cli;
using Microsoft.Extensions.Logging;
using pw4us.Resources;

namespace pw4us.Infrastructure;

public sealed class UnhandledExceptionHandler
{
    public static int OnException(Exception exception, ITypeResolver? resolver)
    {
        var typedLoggerType = typeof(ILogger<>).MakeGenericType(typeof(UnhandledExceptionHandler));

        if (resolver?.Resolve(typedLoggerType) is ILogger<UnhandledExceptionHandler> logger)
        {
            logger.LogError(exception, "An unexpected error occurred. Message: {Message}",exception.Message);
        }

        AnsiConsole.MarkupLine($"[red]{StringsResourse.UnhandledException_Message}.[/]");
        return -1;
    }
}