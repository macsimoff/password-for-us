namespace PasswordForUs.Command.Builder;

public class CopyCommandBuilder: ICommandBuilder
{
    public ICommand Build(string[] commandData)
    {
        return new EmptyCommand();
    }
}