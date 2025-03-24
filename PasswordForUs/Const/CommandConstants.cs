namespace PasswordForUs.Const;

public static class CommandConstants
{
    private const string ExitKey = "exit";
    private const string ExitAlias = "e";
    private const string ImportKey = "import";
    private const string ImportAlias = "i";
    private const string OpenKey = "open";
    private const string OpenAlias = "o";
    public const string FindKey = "find";
    private const string FindAlias = "f";
    private const string HelpKey = "help";
    private const string AddKey = "add";
    private const string GeneratePassKey = "generate";
    private const string GeneratePassAlias = "gen";
    private const string SyncKey = "sync";
    private const string DeleteKey = "delete";
    private const string DeleteAlias = "del";
    private const string ChangePassKey = "change";
    private const string ChangePassAlias = "ch";
    private const string ShowSettingsKey = "show-sttings";
    private const string ShowSettingsAlias = "show-s";
    private const string SetSettingsKey = "set-settings";
    private const string SetSettingsAlias = "set-s";
    private const string SetPassKey = "set-pass";
    private const string SetPassAlias ="set-p";

    public const byte EmptyCode = 0;
    public const byte StopCode = 1;
    public const byte ImportCode = 2;
    public const byte OpenCode = 3;
    public const byte FindCode = 4;
    public const byte HelpCode = 6;
    public const byte AddCode = 7;
    public const byte GeneratePassCode = 8;
    public const byte SynchronizeDataCode = 9;
    public const byte DeleteCode = 10;
    public const byte ChangePassCode = 11;
    public const byte ShowSettingsCode = 12;
    public const byte SetSettingsCode = 13;
    public const byte SetPassCode = 14;

    public static readonly Dictionary<string, byte> CommandCode = new()
    {
        {ExitKey, StopCode},
        {ExitAlias, StopCode},
        {ImportKey, ImportCode},
        {ImportAlias, ImportCode},
        {OpenKey, OpenCode},
        {OpenAlias, OpenCode},
        {FindKey, FindCode},
        {FindAlias, FindCode},
        {HelpKey, HelpCode},
        {AddKey, AddCode},
        {GeneratePassKey, GeneratePassCode},
        {GeneratePassAlias, GeneratePassCode},
        {SyncKey, SynchronizeDataCode},
        {DeleteKey, DeleteCode},
        {DeleteAlias, DeleteCode},
        {ChangePassKey, ChangePassCode},
        {ChangePassAlias, ChangePassCode},
        {ShowSettingsKey, ShowSettingsCode},
        {ShowSettingsAlias, ShowSettingsCode},
        {SetSettingsKey, SetSettingsCode},
        {SetSettingsAlias, SetSettingsCode},
        {SetPassKey, SetPassCode},
        {SetPassAlias, SetPassCode}
    };

    public static readonly Dictionary<byte, string> CommandName = new()
    {
        {StopCode, ExitKey},
        {ImportCode, ImportKey},
        {OpenCode, OpenKey},
        {FindCode, FindKey},
        {HelpCode, HelpKey},
        {AddCode, AddKey},
        {GeneratePassCode, GeneratePassKey},
        {SynchronizeDataCode, SyncKey},
        {DeleteCode, DeleteKey},
        {ChangePassCode, ChangePassKey},
        {ShowSettingsCode, ShowSettingsKey},
        {SetSettingsCode, SetSettingsKey},
        {SetPassCode, SetPassKey}
    };

    public static readonly Dictionary<byte, string> CommandAlias = new()
    {
        {StopCode, ExitAlias},
        {ImportCode, ImportAlias},
        {OpenCode, OpenAlias},
        {FindCode, FindAlias},
        {HelpCode, ""},
        {AddCode, ""},
        {GeneratePassCode, GeneratePassAlias},
        {SynchronizeDataCode, ""},
        {DeleteCode, DeleteAlias},
        {ChangePassCode, ChangePassAlias},
        {ShowSettingsCode, ShowSettingsAlias},
        {SetSettingsCode, SetSettingsAlias},
        {SetPassCode, SetPassAlias}
    };
}