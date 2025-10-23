using Spectre.Console;

namespace pw4us.Utils;

public static class ConsoleUtils
{
    public static Spinner GetSpinnerAnimation()
    {
        return AnsiConsole.Profile.Capabilities.Unicode ? Spinner.Known.Clock : Spinner.Known.Binary;
    }

    public static string GetFloppyEmoji()
    {
        return AnsiConsole.Profile.Capabilities.Unicode ? "💾" : "##";
    }

    public static string GetKeyEmoji()
    {
        return AnsiConsole.Profile.Capabilities.Unicode ? "🔑" : "->";
    }
}