using PasswordForUsLibrary.PassGenerator;

namespace PasswordForUs.Command.Builder;

public class GeneratePassCommandBuilder: ICommandBuilder
{
    public ICommand Build(string[] commandData)
    {
        if(commandData.Length!=0)
            return int.TryParse(commandData[0], out var length) ? BuildCommand(length) : BuildCommand(0);
        return BuildCommand(0);
    }

    private ICommand BuildCommand(int passLength)
    {
        return new GeneratePassCommand(passLength, new PassGenerator());
    }
}