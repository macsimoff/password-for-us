namespace PasswordForUs.Settings;

public class AppSettings
{
    public string Pass { get; set; } = string.Empty;
    public int DefaultPassLength { get; set; } = 12;
    public string[] CharacterSets { get; set; } = {
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
        "012345678901234567890123456789",
        "abcdefghijklmnopqrstuvwxyz",
        "!@#$%^&*_+{}[]|\\:;\"'<>?/~`()_-+="
    };

    public string SyncDataPath { get; set; } = "program.fdb";
    public bool AutoOpenStorage { get; set; }
    public ShowSettings ShowSettings { get; set; } = new();
    public int PassHashIteration { get; set; }
    public string Culture { get; set; } = "en-US";
}