using PasswordForUs.Command;
using PasswordForUs.Const;

namespace PasswordForUs.Model;

public class HelpCommandData
{
    public HelpCommandData(string commandName)
    {
        CommandCode = CommandConstants.CommandCode.GetValueOrDefault(commandName, CommandConstants.EmptyCode);
    }

    public HelpCommandData()
    {
        CommandCode = CommandConstants.EmptyCode;
    }

    public byte CommandCode { get; set; }
}