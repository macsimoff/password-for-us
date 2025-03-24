using PasswordForUs.Command.Builder.DataBuilder;

namespace PasswordForUs.Command.Builder;

public class SetSettingsCommandBuilder : ICommandBuilder
{
    private readonly SetSettingsDataBuilder _dataBuilder = new();
    public ICommand Build(string[] commandData)
    {
        var data = _dataBuilder.CreateCommandData(commandData);

        return new SetSettingsCommand(data);
    }

}