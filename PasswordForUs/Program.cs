using PasswordForUs.Const;
using PasswordForUs.Settings;
using PasswordForUsLibrary.Exception;

namespace PasswordForUs;

static class Program
{
    static void Main()
    {
        Console.WriteLine("--PasswordForUs started");
        
        var settings = AppSettingsBuilder.Build("settings.json");
        if(string.IsNullOrEmpty(settings.Pass))
            TryExecuteCommand(settings, CommandConstants.CommandName[CommandConstants.SetPassCode]);

        if (settings.AutoOpenStorage)
        {
            TryExecuteCommand(settings, CommandConstants.CommandName[CommandConstants.OpenCode]);
        }
        else
        {
            Console.WriteLine("You need to open(o) or to import(i) the file with stared passwords.");
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