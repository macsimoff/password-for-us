using PasswordForUs.Model;
using PasswordForUs.Settings;

namespace PasswordForUs.Command
{
    public class SetSettingsCommand : ICommand
    {
        private readonly SetSettingsCommandData _data;

        public SetSettingsCommand(SetSettingsCommandData data)
        {
            _data = data;
        }

        public void Execute(AppSettings appSettings)
        {
            UpdatePass(appSettings);
            UpdateDefaultPassLength(appSettings);
            UpdateCharacterSets(appSettings);
            UpdateSyncDataPath(appSettings);
            UpdateAutoImport(appSettings);
            UpdateShowSettings(appSettings);
            UpdateCulture(appSettings);

            Console.WriteLine(Resources.Resources.SetSettings_Success);
        }

        private void UpdateCulture(AppSettings appSettings)
        {
            if(_data.Culture != null)
            {
                appSettings.Culture = _data.Culture;
            }
        }

        private void UpdatePass(AppSettings appSettings)
        {
            if (_data.Pass != null)
            {
                appSettings.Pass = _data.Pass;
            }
        }

        private void UpdateDefaultPassLength(AppSettings appSettings)
        {
            if (_data.DefaultPassLength.HasValue)
            {
                appSettings.DefaultPassLength = _data.DefaultPassLength.Value;
            }
        }

        private void UpdateCharacterSets(AppSettings appSettings)
        {
            if (_data.CharacterSets != null)
            {
                appSettings.CharacterSets = _data.CharacterSets;
            }
        }

        private void UpdateSyncDataPath(AppSettings appSettings)
        {
            if (_data.SyncDataPath != null)
            {
                appSettings.SyncDataPath = _data.SyncDataPath;
            }
        }

        private void UpdateAutoImport(AppSettings appSettings)
        {
            if (_data.AutoOpenStorage.HasValue)
            {
                appSettings.AutoOpenStorage = _data.AutoOpenStorage.Value;
            }
        }

        private void UpdateShowSettings(AppSettings appSettings)
        {
            if (_data.Show == null) return;

            var showSettingsData = _data.Show;
            if (showSettingsData.Id.HasValue)
                appSettings.ShowSettings.Id = showSettingsData.Id.Value;
            if (showSettingsData.Url.HasValue)
                appSettings.ShowSettings.Url = showSettingsData.Url.Value;
            if (showSettingsData.User.HasValue)
                appSettings.ShowSettings.User = showSettingsData.User.Value;
            if (showSettingsData.Name.HasValue)
                appSettings.ShowSettings.Name = showSettingsData.Name.Value;
            if (showSettingsData.Login.HasValue)
                appSettings.ShowSettings.Login = showSettingsData.Login.Value;
            if (showSettingsData.Password.HasValue)
                appSettings.ShowSettings.Password = showSettingsData.Password.Value;
            if (showSettingsData.All.HasValue)
                appSettings.ShowSettings.All = showSettingsData.All.Value;
            if (showSettingsData.AllData.HasValue)
                appSettings.ShowSettings.AllData = showSettingsData.AllData.Value;
            if (showSettingsData.DataNames is { Length: > 0 })
            {
                var oldNames =
                    new Dictionary<string, bool>(
                        appSettings.ShowSettings.DataNames.Select(x => new KeyValuePair<string, bool>(x, true)));
                foreach (var newDataNameValue in showSettingsData.DataNames)
                {
                    oldNames[newDataNameValue.Key] = newDataNameValue.Value;
                }

                appSettings.ShowSettings.DataNames = oldNames.Where(x => x.Value)
                                                             .Select(x => x.Key)
                                                             .ToArray();
            }
        }
    }
}