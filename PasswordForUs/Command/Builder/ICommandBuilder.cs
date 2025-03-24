namespace PasswordForUs.Command.Builder;

public interface ICommandBuilder
{
    ICommand Build(string[] commandData);
}