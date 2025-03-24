namespace PasswordForUs.Command.Builder;

public class ShowSettingsCommandBuilder: ICommandBuilder
{
    public ICommand Build(string[] commandData)
    {
        return new ShowSettingsCommand();
    }
}