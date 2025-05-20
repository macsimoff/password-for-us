using PasswordForUs.Const;
using PasswordForUs.Model;

namespace PasswordForUs.Command.Builder;

public class HelpCommandBuilder: ICommandBuilder
{
    public ICommand Build(string[] commandData)
    {
        var data = GetCommandData(commandData);
        return GetCommand(data);
    }

    private HelpCommandData GetCommandData(string[] commandData)
    {
        if (commandData.Length <= 0) return new HelpCommandData();
        
        var res = new HelpCommandData(commandData[0]);
        if(res.CommandCode == CommandConstants.EmptyCode) 
            Console.WriteLine(Resources.Resources.Help_InvalidСommandName);
            
        return res;
    }

    private ICommand GetCommand(HelpCommandData data)
    {
        return new HelpCommand(data);
    }
}