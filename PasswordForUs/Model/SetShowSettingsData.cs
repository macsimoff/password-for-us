namespace PasswordForUs.Model;

public class SetShowSettingsData
{
    public bool? All { get; set; }
    public bool? Id { get; set; }
    public bool? Url { get; set; }
    public bool? User { get; set; }
    public bool? Name { get; set; }
    public bool? Login { get; set; }
    public bool? Password { get; set; }
    public bool? AllData { get; set; }
    public KeyValuePair<string, bool>[]? DataNames { get; set; }
}