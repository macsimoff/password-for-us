namespace PasswordForUs.Const;

public static class HelpConstants
{
    public static readonly Dictionary<byte, string> CommandHelp = new()
    {
        {CommandConstants.StopCode, "stop the program"},
        {CommandConstants.ImportCode, "parsing file and add this information to storage and synchronise it"},
        {CommandConstants.OpenCode, "open the storage file"},
        {CommandConstants.FindCode, "find the entry in the storage by Name or Url"},
        //{CopyCode, "Copies the value entry to the clipboard."},
        {CommandConstants.HelpCode, "show help information, for more about command use 'help [command_name]'"},
        {CommandConstants.AddCode, "add a new entry to the storage."},
        {CommandConstants.GeneratePassCode, "generate a random password."},
        {CommandConstants.SynchronizeDataCode, "synchronize the storage with the data file"},
        {CommandConstants.DeleteCode, "delete the entry from the storage."},
        {CommandConstants.ChangePassCode, "change an entry in the storage"},
        {CommandConstants.ShowSettingsCode, "show the settings"},
        {CommandConstants.SetSettingsCode, "set the settings"},
        {CommandConstants.SetPassCode, "set the password"},
    };
    
    public static readonly Dictionary<byte, string> CommandFullHelp = new()
    {
        {CommandConstants.StopCode, $"""
                                     Usage: {CommandConstants.CommandName[CommandConstants.StopCode]}
                                     stop the program.
                                     alias: {CommandConstants.CommandAlias[CommandConstants.StopCode]}
                                     """},
        {CommandConstants.ImportCode, $"""
                                       Usage: {CommandConstants.CommandName[CommandConstants.ImportCode]} <filePath>
                                       parsing file and add this information to storage and synchronise it.
                                       alias: {CommandConstants.CommandAlias[CommandConstants.ImportCode]}
                                       """},
        {CommandConstants.OpenCode, $"""
                                     Usage: {CommandConstants.CommandName[CommandConstants.OpenCode]} <filePath>
                                     open the storage file.
                                     alias: {CommandConstants.CommandAlias[CommandConstants.OpenCode]}
                                     """},
        {CommandConstants.FindCode, $"""
                                     Usage: {CommandConstants.FindKey} <text>
                                          or
                                            {CommandConstants.FindKey} [<{FindKeyConst.IdKey}/{FindKeyConst.IdAlias}> <idValue>]
                                                 [<{FindKeyConst.UrlKey}/{FindKeyConst.UrlAlias}> <urlValue>]
                                                 [<{FindKeyConst.NameKey}/{FindKeyConst.NameAlias}> <nameValue>]
                                     find the entry in the storage by Name or Url. If you specify the --id option, you can search by id.
                                     alias: {CommandConstants.CommandAlias[CommandConstants.FindCode]}
                                     """},
        //{CopyCode, "Usage: copy <--id/-i> <idValue>\nCopies the value entry to the clipboard."},
        {CommandConstants.HelpCode, $"""
                                     Usage: help [command_name]
                                     show the help information.
                                     """},
        {CommandConstants.AddCode, $"""
                                    Usage: {CommandConstants.CommandName[CommandConstants.AddCode]}
                                          [<{PassCommandConst.UserKey}/{PassCommandConst.UserAlias}> <userValue>]
                                          [<{PassCommandConst.UrlKey}/{PassCommandConst.UrlAlias}> <urlValue>]
                                          [<{PassCommandConst.NameKey}/{PassCommandConst.NameAlias}> <nameValue>]
                                          [<{PassCommandConst.PassKey}/{PassCommandConst.PassAlias}> <passValue>]
                                          [<{PassCommandConst.LoginKey}/{PassCommandConst.LoginAlias}> <loginValue>]
                                          [--key1 <value1>] [--key2 <value2> ...]
                                    add a new value entry to the storage, like url, login, password, etc..
                                    alias: {CommandConstants.CommandAlias[CommandConstants.AddCode]}
                                    """},
        {CommandConstants.GeneratePassCode, $"""
                                             Usage: {CommandConstants.CommandName[CommandConstants.GeneratePassCode]} [length]
                                             generate a random password of the specified length. If no length is provided, a default length of 12 is used.
                                             alias: {CommandConstants.CommandAlias[CommandConstants.GeneratePassCode]}
                                             """},
        {CommandConstants.SynchronizeDataCode, $"""
                                                Usage: {CommandConstants.CommandName[CommandConstants.SynchronizeDataCode]}
                                                synchronizes the storage.
                                                alias: {CommandConstants.CommandAlias[CommandConstants.SynchronizeDataCode]}
                                                """},
        {CommandConstants.DeleteCode, $"""
                                       Usage: {CommandConstants.CommandName[CommandConstants.DeleteCode]} <--id/-i> <idValue>
                                       delete the value entry from the storage.
                                       alias: {CommandConstants.CommandAlias[CommandConstants.DeleteCode]}
                                       """},
        {CommandConstants.ChangePassCode, $"""
                                           Usage: {CommandConstants.CommandName[CommandConstants.ChangePassCode]}
                                                 <{PassCommandConst.IdKey}/{PassCommandConst.IdAlias}> <idValue>
                                                 [<{PassCommandConst.UserKey}/{PassCommandConst.UserAlias}> <userValue>]
                                                 [<{PassCommandConst.UrlKey}/{PassCommandConst.UrlAlias}> <urlValue>]
                                                 [<{PassCommandConst.NameKey}/{PassCommandConst.NameAlias}> <nameValue>]
                                                 [<{PassCommandConst.PassKey}/{PassCommandConst.PassAlias}> <passValue>]
                                                 [<{PassCommandConst.LoginKey}/{PassCommandConst.LoginAlias}> <loginValue>]
                                                 [--key1 <value1>] [--key2 <value2> ...]
                                           change an entry in the storage, like url, login, password, etc..
                                           alias: {CommandConstants.CommandAlias[CommandConstants.ChangePassCode]}
                                           """},
        {CommandConstants.ShowSettingsCode, $"""
                                             Usage: {CommandConstants.CommandName[CommandConstants.ShowSettingsCode]}
                                             shows the settings.
                                             alias: {CommandConstants.CommandAlias[CommandConstants.ShowSettingsCode]}
                                             """},
        {CommandConstants.SetSettingsCode, $"""
Usage: {CommandConstants.CommandName[CommandConstants.SetSettingsCode]}
      [<{SetSettingsKeyConst.PassKey}/{SetSettingsKeyConst.PassAlias}> <password>]
      [<{SetSettingsKeyConst.PassLengthKey}/{SetSettingsKeyConst.PassLengthAlias}> <length>]
      [<{SetSettingsKeyConst.CharSetsKey}/{SetSettingsKeyConst.CharSetsAlias}> <value1> <value2> ...]
      [<{SetSettingsKeyConst.SyncPathKey}/{SetSettingsKeyConst.SyncPathAlias}> <path>]
      [<{SetSettingsKeyConst.ImportPathKey}/{SetSettingsKeyConst.ImportPathAlias}> <path>]
      [<{SetSettingsKeyConst.AutoImportKey}/{SetSettingsKeyConst.AutoImportAlias}> <y/n>]
      [<{SetShowSettingsKeyConst.AllKey}/{SetShowSettingsKeyConst.AllAlias}> <y/n>]
      [<{SetShowSettingsKeyConst.IdKey}/{SetShowSettingsKeyConst.IdAlias}> <y/n>]
      [<{SetShowSettingsKeyConst.UrlKey}>/<{SetShowSettingsKeyConst.UrlAlias}> <y/n>]
      [<{SetShowSettingsKeyConst.UserKey}/{SetShowSettingsKeyConst.UserAlias}> <y/n>]
      [<{SetShowSettingsKeyConst.NameKey}/{SetShowSettingsKeyConst.NameAlias}> <y/n>]
      [<{SetShowSettingsKeyConst.LoginKey}/{SetShowSettingsKeyConst.LoginAlias}> <y/n>]
      [<{SetShowSettingsKeyConst.PassKey}/{SetShowSettingsKeyConst.PassAlias}> <y/n>]
      [<{SetShowSettingsKeyConst.AllDataKey}/{SetShowSettingsKeyConst.AllDataAlias}> <y/n>]
      [--key1 <y/n>] [--key2 <y/n>...]]
set the settings. If you specify the --show option, you can configure which fields to display.
command '{CommandConstants.CommandName[CommandConstants.SetSettingsCode]} --show-data y' - shows all data fields.
alias: {CommandConstants.CommandAlias[CommandConstants.SetSettingsCode]}
"""},
        {CommandConstants.SetPassCode, $"""
                                        Usage: {CommandConstants.CommandName[CommandConstants.SetPassCode]} <passValue>
                                        set the password.
                                        alias: {CommandConstants.CommandAlias[CommandConstants.SetPassCode]}
                                        """},
    };
}