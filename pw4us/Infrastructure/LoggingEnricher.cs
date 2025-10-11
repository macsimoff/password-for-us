using Serilog.Core;
using Serilog.Events;

namespace pw4us.Infrastructure;

internal class LoggingEnricher : ILogEventEnricher
{
    private string _cachedLogFilePath = string.Empty;
    private LogEventProperty? _cachedLogFilePathProperty;

    public static string Path = string.Empty;

    public const string LogFilePathPropertyName = "log";

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        LogEventProperty logFilePathProperty;

        if (_cachedLogFilePathProperty != null && Path.Equals(_cachedLogFilePath))
        {
            logFilePathProperty = _cachedLogFilePathProperty;
        }
        else
        {
            _cachedLogFilePath = Path;
            _cachedLogFilePathProperty = logFilePathProperty = propertyFactory.CreateProperty(LogFilePathPropertyName, Path);
        }

        logEvent.AddPropertyIfAbsent(logFilePathProperty);
    }
}