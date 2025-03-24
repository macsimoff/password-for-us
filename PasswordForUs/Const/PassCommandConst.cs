namespace PasswordForUs.Const;

public static class PassCommandConst
{
    public const string IdKey = "--id";
    public const string IdAlias = "-i";
    public const string UserKey = "--user";
    public const string UserAlias = "-u";
    public const string UrlKey = "--url";
    public const string UrlAlias = "-r";
    public const string NameKey = "--name";
    public const string NameAlias = "-n";
    public const string PassKey = "--pass";
    public const string PassAlias = "-p";
    public const string LoginKey = "--login";
    public const string LoginAlias = "-l";
    
    public const byte IdCode = 1;
    public const byte UserCode = 2;
    public const byte UrlCode = 3;
    public const byte NameCode = 4;
    public const byte PassCode = 5;
    public const byte LoginCode = 6;

    public static readonly Dictionary<string, byte> KeyCode = new()
    {
        { IdKey, IdCode },
        { IdAlias, IdCode },
        { UserKey, UserCode },
        { UserAlias, UserCode },
        { UrlKey, UrlCode },
        { UrlAlias, UrlCode },
        { NameKey, NameCode },
        { NameAlias, NameCode },
        { PassKey, PassCode },
        { PassAlias, PassCode },
        { LoginKey, LoginCode },
        { LoginAlias, LoginCode }
    };

    public static readonly Dictionary<byte, string> KeyName = new()
    {
        { IdCode, IdKey },
        { UserCode, UserKey },
        { UrlCode, UrlKey },
        { NameCode, NameKey },
        { PassCode, PassKey },
        { LoginCode, LoginKey }
    };

    public static readonly Dictionary<byte, string> KeyAlias = new()
    {
        { IdCode, IdAlias },
        { UserCode, UserAlias },
        { UrlCode, UrlAlias },
        { NameCode, NameAlias },
        { PassCode, PassAlias },
        { LoginCode, LoginAlias }
    };
}