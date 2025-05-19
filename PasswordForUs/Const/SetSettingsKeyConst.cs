namespace PasswordForUs.Const;

public static class SetSettingsKeyConst
{
    public const string PassLengthKey = "--pass-length";
    public const string PassLengthAlias = "-l";
    public const string CharSetsKey = "--char-sets";
    public const string CharSetsAlias = "-cs";
    public const string SyncPathKey = "--sync-path";
    public const string SyncPathAlias = "-spath";
    public const string ImportPathKey = "--import-path";
    public const string ImportPathAlias = "-ip";
    public const string AutoImportKey = "--auto-import";
    public const string AutoImportAlias = "-ai";
    public const string PassKey = "--pass";
    public const string PassAlias = "-p";
    public const string CultureKey = "--culture";
    public const string CultureAlias = "-c";
    

    public const byte PassLengthCode = 1;
    public const byte CharSetsCode = 2;
    public const byte SyncPathCode = 3;
    public const byte ImportPathCode = 4;
    public const byte AutoImportCode = 5;
    public const byte PassCode = 6;
    public const byte CultureCode = 7;

    public static readonly Dictionary<string, byte> KeyCode = new()
    {
        {PassLengthKey, PassLengthCode},
        {PassLengthAlias, PassLengthCode},
        {CharSetsKey, CharSetsCode},
        {CharSetsAlias, CharSetsCode},
        {SyncPathKey, SyncPathCode},
        {SyncPathAlias, SyncPathCode},
        {ImportPathKey, ImportPathCode},
        {ImportPathAlias, ImportPathCode},
        {AutoImportKey, AutoImportCode},
        {AutoImportAlias, AutoImportCode},
        {PassKey, PassCode},
        {PassAlias, PassCode},
        {CultureKey, CultureCode},
        {CultureAlias, CultureCode},
    };

    public static readonly Dictionary<byte, string> KeyName = new()
    {
        {PassLengthCode, PassLengthKey},
        {CharSetsCode, CharSetsKey},
        {SyncPathCode, SyncPathKey},
        {ImportPathCode, ImportPathKey},
        {AutoImportCode, AutoImportKey},
        {PassCode, PassKey},
        {CultureCode, CultureKey},
    };

    public static readonly Dictionary<byte, string> KeyAlias = new()
    {
        {PassLengthCode, PassLengthAlias},
        {CharSetsCode, CharSetsAlias},
        {SyncPathCode, SyncPathAlias},
        {ImportPathCode, ImportPathAlias},
        {AutoImportCode, AutoImportAlias},
        {PassCode, PassAlias},
        {CultureCode, CultureAlias},
    };
}