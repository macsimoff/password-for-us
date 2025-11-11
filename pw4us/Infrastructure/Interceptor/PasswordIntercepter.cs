using pw4us.Infrastructure.Settings;
using pw4us.Rendering;
using Spectre.Console;
using Spectre.Console.Cli;

namespace pw4us.Infrastructure.Interceptor;

public class PasswordInterceptor : ICommandInterceptor
{
    public void Intercept(CommandContext context, CommandSettings settings)
    {
        if (settings is not PassCommandSettings passSettings) return;
        if (!string.IsNullOrEmpty(passSettings.Pass)) return;
        
        var keyEmoji = AnsiConsoleHelpers.GetKeyEmoji();
        passSettings.Pass = AnsiConsole.Prompt(
            new TextPrompt<string>($"{keyEmoji} Enter the PASSWORD : ").Secret());
    }
}