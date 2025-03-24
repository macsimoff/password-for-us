namespace PasswordForUs.Command.Builder;

public class SetPassCommandBuilder: ICommandBuilder
{
    public ICommand Build(string[] commandData)
    {
        return commandData.Length > 0 ? new SetPassCommand(commandData[0]) : new SetPassCommand(string.Empty);
    }
}