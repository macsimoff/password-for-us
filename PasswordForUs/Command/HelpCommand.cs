using PasswordForUs.ConsoleExtension;
using PasswordForUs.Const;
using PasswordForUs.Model;
using PasswordForUs.Settings;

namespace PasswordForUs.Command;

public class HelpCommand: ICommand
{
    private readonly HelpCommandData _data;

    public HelpCommand(HelpCommandData data)
    {
        _data = data;
    }

    public void Execute(AppSettings appSettings)
    {
        if (_data.CommandCode == CommandConstants.EmptyCode)
        {
            var nameLineLength = GetMaxNameLength(CommandConstants.CommandName.Values) + 1;

            foreach (var b in CommandConstants.CommandName)
            {
                Console.Write(b.Value);
                AppConsoleExtension.WriteGap(nameLineLength - b.Value.Length);
                Console.Write(HelpConstants.CommandHelp[b.Key]);
                Console.Write("\n");
            }
        }
        else
        {
            Console.Write(HelpConstants.CommandFullHelp[_data.CommandCode]);
            Console.Write("\n");
        }
    }

    private int GetMaxNameLength(Dictionary<byte, string>.ValueCollection commandCode)
    {
        return commandCode.Select(x => x.Length).Prepend(0).Max();
    }
}