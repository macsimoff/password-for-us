using Microsoft.VisualBasic;
using PasswordForUs.Settings;

namespace PasswordForUs.Command;

public class EmptyCommand: ICommand
{

    public void Execute(AppSettings appSettings)
    {
        Console.WriteLine(Resources.Resources.Empty_TryingToExecute);
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