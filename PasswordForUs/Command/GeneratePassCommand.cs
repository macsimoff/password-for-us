using PasswordForUs.Settings;
using PasswordForUsLibrary.PassGenerator;

namespace PasswordForUs.Command;

public class GeneratePassCommand : ICommand
{
    private readonly int _passLength;
    private readonly PassGenerator _passGenerator;

    public GeneratePassCommand(int passLength, PassGenerator passGenerator)
    {
        _passLength = passLength;
        _passGenerator = passGenerator;
    }

    public void Execute(AppSettings appSettings)
    {
        var password = _passGenerator.Generate(_passLength>0 ? _passLength : appSettings.DefaultPassLength, 
                                                    appSettings.CharacterSets);
        Console.WriteLine(Resources.Resources.GeneratePass_GeneratePass, password);
    }
}