using PasswordForUs.Settings;

namespace PasswordForUs.Model;

public class SetSettingsCommandData
{
    public int? DefaultPassLength { get; set; }
    public string[]? CharacterSets { get; set; }
    public string? SyncDataPath { get; set; }
    public string? DefaultImportPath { get; set; }
    public bool? AutoOpenStorage { get; set; }
    public SetShowSettingsData? Show { get; set; }
    public string? Pass { get; set; }
}