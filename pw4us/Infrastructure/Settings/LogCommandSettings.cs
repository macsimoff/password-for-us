using System.ComponentModel;
using System.Globalization;
using pw4us.Attributes;
using pw4us.Resources;
using Serilog.Events;
using Spectre.Console.Cli;

namespace pw4us.Infrastructure.Settings;

public class LogCommandSettings : CommandSettings
{
    [CommandOption("--logFile", IsHidden = true)]
    [LocalizedDescription(nameof(DescriptionResources.LogSettings_LogFile))]
    public string LogFile { get; set; } = string.Empty;

    [CommandOption("--logLevel", IsHidden = true)]
    [LocalizedDescription(nameof(DescriptionResources.LogSettings_LogLevel))]
    [TypeConverter(typeof(VerbosityConverter))]
    public LogEventLevel? LogLevel { get; set; }
}

public sealed class VerbosityConverter : TypeConverter
{
    private static readonly Dictionary<string, LogEventLevel> Lookup = new(StringComparer.OrdinalIgnoreCase)
    {
        {"d", LogEventLevel.Debug},
        {"v", LogEventLevel.Verbose},
        {"i", LogEventLevel.Information},
        {"w", LogEventLevel.Warning},
        {"e", LogEventLevel.Error},
        {"f", LogEventLevel.Fatal}
    };

    public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is not string stringValue)
            throw new NotSupportedException("Can't convert value to verbosity.");

        var result = Lookup.TryGetValue(stringValue, out var verbosity);
        if (result) return verbosity;

        const string format = "The value '{0}' is not a valid verbosity.";
        var message = string.Format(CultureInfo.InvariantCulture, format, value);
        throw new InvalidOperationException(message);
    }
}