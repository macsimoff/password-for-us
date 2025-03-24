using PasswordForUs.Command.Builder;
using PasswordForUs.Command.Builder.Factory.Repo;
using PasswordForUs.Command.Builder.Factory.Security;
using PasswordForUs.Command.Builder.Factory.Sync;
using PasswordForUs.Const;

namespace PasswordForUs;

public static class CommandBuilder
{
    public static ICommand Build(string? commandText)
    {
        var (commandName, commandData) = ParsingCommand(commandText);
        var builder = GetCommandBuilder(commandName);
        return builder.Build(commandData);
    }

    private static ICommandBuilder GetCommandBuilder(byte code)
    {
        var repoFac = new MemoryStorageRepositoryFactory();
        var syncFac = new EncryptingFileSynchronizerFactory();
        var securityFac = new Pbkdf2SecurityFactory();
        return code switch
        {
            CommandConstants.EmptyCode => new EmptyCommandBuilder(),
            CommandConstants.StopCode => new StopCommandBuilder(),
            CommandConstants.ImportCode => new ImportCommandBuilder(repoFac, syncFac, securityFac),
            CommandConstants.OpenCode => new OpenCommandBuilder(repoFac, syncFac, securityFac),
            CommandConstants.FindCode => new FindCommandBuilder(repoFac),
            //CommandConstants.CopyCode => new CopyCommandBuilder(),// todo:возможно нужно делать через команду Fined
            CommandConstants.HelpCode => new HelpCommandBuilder(),
            CommandConstants.AddCode => new AddCommandBuilder(repoFac, syncFac, securityFac),
            CommandConstants.GeneratePassCode => new GeneratePassCommandBuilder(),
            CommandConstants.SynchronizeDataCode => new SynchronizeDataCommandBuilder(repoFac, syncFac, securityFac),
            CommandConstants.DeleteCode => new DeleteCommandBuilder(repoFac, syncFac, securityFac),
            CommandConstants.ChangePassCode => new ChangePassCommandBuilder(repoFac, syncFac, securityFac),
            CommandConstants.ShowSettingsCode => new ShowSettingsCommandBuilder(),
            CommandConstants.SetSettingsCode => new SetSettingsCommandBuilder(),
            CommandConstants.SetPassCode => new SetPassCommandBuilder(),
            _ => new EmptyCommandBuilder()
        };
    }
    private static (byte commandCode, string[] commandData) ParsingCommand(string? commandText)
    {
        if (commandText == null) return (0, Array.Empty<string>());
        
        var list = commandText.Split(' ').Select(x => x.Trim()).ToList();
        
        return CommandConstants.CommandCode.TryGetValue(list[0], out var code) 
            ? ( code, list.GetRange(1, list.Count - 1).ToArray()) 
            : ((byte)0, Array.Empty<string>());
    }
}