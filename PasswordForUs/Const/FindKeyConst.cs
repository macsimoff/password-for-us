namespace PasswordForUs.Const;

public static class FindKeyConst
{
    public const string IdKey = "--id";
    public const string IdAlias = "-i";
    public const string UrlKey = "--url";
    public const string UrlAlias = "-u";
    public const string NameKey = "--name";
    public const string NameAlias = "-n";
    
    public const byte IdCode = 1;
    public const byte UrlCode = 2;
    public const byte NameCode = 3;

    public static readonly Dictionary<string, byte> KeyCode = new()
    {
        { IdKey, IdCode },
        { IdAlias, IdCode },
        { UrlKey, UrlCode },
        { UrlAlias, UrlCode },
        { NameKey, NameCode },
        { NameAlias, NameCode }
    };

    public static readonly Dictionary<byte, string> KeyName = new()
    {
        { IdCode, IdKey },
        { UrlCode, UrlKey },
        { NameCode, NameKey }
    };

    public static readonly Dictionary<byte, string> KeyAlias = new()
    {
        { IdCode, IdAlias },
        { UrlCode, UrlAlias },
        { NameCode, NameAlias }
    };
}