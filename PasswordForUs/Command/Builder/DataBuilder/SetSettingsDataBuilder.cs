using PasswordForUs.Const;
using PasswordForUs.Model;

namespace PasswordForUs.Command.Builder.DataBuilder;

public class SetSettingsDataBuilder

{
    private const string TrueFlag = "y";
    private const string FalseFlag = "n";

    public SetSettingsCommandData CreateCommandData(string[] commandData)
    {
        var settings = new SetSettingsCommandData();
        var showSettings = new SetShowSettingsData();
        var dataNames = new List<KeyValuePair<string, bool>>();
        int i = 0;

        while (i < commandData.Length)
        {
            if (i + 1 >= commandData.Length)
                throw new ArgumentException("Invalid command data: missing value for the last key.");

            var key = commandData[i];
            var value = commandData[++i];

            if (SetSettingsKeyConst.KeyCode.TryGetValue(key, out var keyValue)
                && keyValue == SetSettingsKeyConst.CharSetsCode)
            {
                i = SetCharSetValue(commandData, i, settings);
                continue;
            }
            
            if (SetSettingsKeyConst.KeyCode.ContainsKey(key))
            {
                SetSettingsValue(settings, key, value);
                i++;
                continue;
            }
            
            if (SetShowSettingsKeyConst.KeyCode.ContainsKey(key))
            {
                SetShowSettingsValue(showSettings, key, value);
                i++;
                continue;
            }

            if (key.StartsWith('-') || key.StartsWith("--"))
            {
                SetDataNamesValue(dataNames,key, value);
                i++;
                continue;
            }
            
            throw new ArgumentException($"Invalid key: {key}");
        }

        settings.Show = showSettings;
        settings.Show.DataNames = dataNames.ToArray();
        return settings;
    }

    private void SetDataNamesValue(List<KeyValuePair<string, bool>> names, string dataName, string value)
    {
        if(ParseFlag(value) != null)
            names.Add(new KeyValuePair<string, bool>(dataName.TrimStart('-'), GetFlag(value)));
        else 
            throw new ArgumentException($"Invalid value for {dataName}: {value}. Expected 'y' or 'n'.");
    }

    private static int SetCharSetValue(string[] commandData, int i, SetSettingsCommandData settings)
    {
        var charSets = new List<string>();
        while (i < commandData.Length
               && !SetSettingsKeyConst.KeyCode.ContainsKey(commandData[i])
               && !SetShowSettingsKeyConst.KeyCode.ContainsKey(commandData[i]))
        {
            charSets.Add(commandData[i]);
            i++;
        }

        settings.CharacterSets = charSets.ToArray();
        return i;
    }

    private void SetShowSettingsValue(SetShowSettingsData showSettings, string key, string value)
    {
        var code = SetShowSettingsKeyConst.KeyCode[key];
        switch (code)
        {
            case SetShowSettingsKeyConst.AllCode:
                showSettings.All = ParseFlag(value);
                break;
            case SetShowSettingsKeyConst.IdCode:
                showSettings.Id = ParseFlag(value);
                break;
            case SetShowSettingsKeyConst.UrlCode:
                showSettings.Url = ParseFlag(value);
                break;
            case SetShowSettingsKeyConst.UserCode:
                showSettings.User = ParseFlag(value);
                break;
            case SetShowSettingsKeyConst.NameCode:
                showSettings.Name = ParseFlag(value);
                break;
            case SetShowSettingsKeyConst.LoginCode:
                showSettings.Login = ParseFlag(value);
                break;
            case SetShowSettingsKeyConst.PassCode:
                showSettings.Password = ParseFlag(value);
                break;
            case SetShowSettingsKeyConst.AllDataCode:
                showSettings.AllData = ParseFlag(value);
                break;
        }
    }

    private void SetSettingsValue(SetSettingsCommandData settings, string key, string value)
    {
        var code = SetSettingsKeyConst.KeyCode[key];
        switch (code)
        {
            case SetSettingsKeyConst.PassCode:
                settings.Pass = value;
                break;
            case SetSettingsKeyConst.PassLengthCode:
                settings.DefaultPassLength = byte.Parse(value);
                break;
            case SetSettingsKeyConst.SyncPathCode:
                settings.SyncDataPath = value;
                break;
            case SetSettingsKeyConst.ImportPathCode:
                settings.DefaultImportPath = value;
                break;
            case SetSettingsKeyConst.AutoImportCode:
                settings.AutoOpenStorage = ParseFlag(value);
                break;
            case SetSettingsKeyConst.CultureCode:
                settings.Culture = value;
                break;
            default:
                throw new ArgumentException($"Invalid key: {key}");
        }
    }

    private bool? ParseFlag(string flag)
    {
        return flag switch
        {
            TrueFlag => true,
            FalseFlag => false,
            _ => null
        };
    }

    private bool GetFlag(string value)
    {
        return value == TrueFlag;
    }
}