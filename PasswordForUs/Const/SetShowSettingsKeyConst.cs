namespace PasswordForUs.Const;

public static class SetShowSettingsKeyConst
{
    public const string IdKey = "--show-id";
    public const string IdAlias = "-si";
    public const string UrlKey = "--show-url";
    public const string UrlAlias = "-sr";
    public const string UserKey = "--show-user";
    public const string UserAlias = "-su";
    public const string NameKey = "--show-name";
    public const string NameAlias = "-sn";
    public const string LoginKey = "--show-login";
    public const string LoginAlias = "-sl";
    public const string PassKey = "--show-pass";
    public const string PassAlias = "-sp";
    public const string AllDataKey = "--show-all-data";
    public const string AllDataAlias = "-sd";
    public const string AllKey = "--show-all";
    public const string AllAlias = "-sa";

    public const byte IdCode = 1;
    public const byte UrlCode = 2;
    public const byte UserCode = 3;
    public const byte NameCode = 4;
    public const byte LoginCode = 5;
    public const byte PassCode = 6;
    public const byte AllDataCode = 7;
    public const byte AllCode = 8;

    public static readonly Dictionary<string, byte> KeyCode = new()
    {
        { IdKey, IdCode },
        { IdAlias, IdCode },
        { UrlKey, UrlCode },
        { UserKey, UserCode },
        { UserAlias, UserCode },
        { NameKey, NameCode },
        { NameAlias, NameCode },
        { LoginKey, LoginCode },
        { LoginAlias, LoginCode },
        { PassKey, PassCode },
        { PassAlias, PassCode },
        { AllDataKey, AllDataCode },
        { AllDataAlias, AllDataCode },
        { AllKey, AllCode },
        { AllAlias, AllCode }
    };

    public static readonly Dictionary<byte, string> KeyName = new()
    {
        { IdCode, IdKey },
        { UrlCode, UrlKey },
        { UserCode, UserKey },
        { NameCode, NameKey },
        { LoginCode, LoginKey },
        { PassCode, PassKey },
        { AllDataCode, AllDataKey },
        { AllCode, AllKey }
    };

    public static readonly Dictionary<byte, string> KeyAlias = new()
    {
        { IdCode, IdAlias },
        { UserCode, UserAlias },
        { NameCode, NameAlias },
        { LoginCode, LoginAlias },
        { PassCode, PassAlias },
        { AllDataCode, AllDataAlias },
        { AllCode, AllAlias }
    };
}