using Microsoft.VisualBasic;
using PasswordForUs.Settings;

namespace PasswordForUs.Command;

public class EmptyCommand: ICommand
{

    public void Execute(AppSettings appSettings)
    {
        Console.WriteLine("The program is trying to execute an empty command - something is wrong!");
    }

    public string GetHelp()
    {
        return string.Empty;
    }

    public string GetFullHelpInstruction()
    {
        return string.Empty;
    }
}