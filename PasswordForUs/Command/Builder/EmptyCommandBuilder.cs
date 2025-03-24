namespace PasswordForUs.Command.Builder;

public class EmptyCommandBuilder:ICommandBuilder
{
    public ICommand Build(string[] commandData)
    {
        return BuildEmptyCommand();
    }
    
    private static ICommand BuildEmptyCommand()
    {
        return new EmptyCommand();
    }
}