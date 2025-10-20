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
        //todo: add resolver for ExceptionHandler on git hub spectreconsole
        if (resolver?.Resolve(typedLoggerType) is ILogger<UnhandledExceptionHandler> logger)
        {
            logger.LogError(exception, "An unexpected error occurred. Message: {Message}",exception.Message);
        }

        AnsiConsole.MarkupLine(exception is CommandParseException
            or CommandAppException
            ? $"[red]{StringsResourse.Error}![/] Message: {exception.Message}"
            : $"[red]{StringsResourse.UnhandledException_Message}.[/]");

        return -1;
    }
}