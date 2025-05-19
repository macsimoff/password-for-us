using System.Globalization;
using PasswordForUs.Const;
using PasswordForUs.Settings;
using PasswordForUsLibrary.Exception;

namespace PasswordForUs;

static class Program
{
    static void Main()
    {
        var settings = AppSettingsBuilder.Build("settings.json");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(settings.Culture);
        
        Console.WriteLine(Resources.Resources.WelcomeMessage);

        if(string.IsNullOrEmpty(settings.Pass))
            TryExecuteCommand(settings, CommandConstants.CommandName[CommandConstants.SetPassCode]);

        if (settings.AutoOpenStorage)
        {
            TryExecuteCommand(settings, CommandConstants.CommandName[CommandConstants.OpenCode]);
        }
        else
        {
            Console.WriteLine(Resources.Resources.OpenFilePrompt);
        }

        while (true)
        {
            if (Console.KeyAvailable)
            {
                TryExecuteCommand(settings, Console.ReadLine());
            }
            Thread.Sleep(700);
        }
    }

    private static void TryExecuteCommand(AppSettings settings, string? input)
    {
        var repeatCount = 0;
        while (true)
        {
            if(repeatCount>2)
                break;
            try
            {
                if(Thread.CurrentThread.CurrentUICulture.Name != settings.Culture)
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(settings.Culture);
                var command = CommandBuilder.Build(input);
                command.Execute(settings);
                break;
            }
            catch (PassInvalidException e)
            {
                Console.WriteLine(e.Message);
                TryExecuteCommand(settings, CommandConstants.CommandName[CommandConstants.SetPassCode]);
                repeatCount++;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                break;
            }
        }
    }
}