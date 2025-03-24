using PasswordForUs.Settings;

namespace PasswordForUs.Command;
/// <summary>
/// Command to stop a console application
/// </summary>
public class StopCommand: ICommand
{
    public void Execute(AppSettings appSettings)
    {
        System.Environment.Exit(0);
    }
}