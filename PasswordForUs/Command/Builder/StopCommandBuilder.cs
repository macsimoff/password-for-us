namespace PasswordForUs.Command.Builder;

public class StopCommandBuilder: ICommandBuilder
{
    public ICommand Build(string[] commandData)
    {
        return BuildStopCommand();
    }
    
    private static ICommand BuildStopCommand()
    {
        return new StopCommand();
    }
}