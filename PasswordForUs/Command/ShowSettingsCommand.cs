using PasswordForUs.Settings;

namespace PasswordForUs.Command
{
    public class ShowSettingsCommand : ICommand
    {
        public void Execute(AppSettings appSettings)
        {
            Console.WriteLine(Resources.Resources.ShowSettings_CurrentSettings);
            Console.WriteLine($"AutoOpenStorage: {appSettings.AutoOpenStorage}");
            Console.WriteLine($"DefaultPassLength: {appSettings.DefaultPassLength}");
            Console.WriteLine("CharacterSets:");
            foreach (var set in appSettings.CharacterSets)
            {
                Console.WriteLine($"  {set}");
            }

            Console.WriteLine($"SyncDataPath: {appSettings.SyncDataPath}");
            Console.WriteLine($"CultureInfo: {appSettings.Culture}");
            Console.WriteLine("Show Settings:");
            Console.WriteLine($"  All: {appSettings.ShowSettings.All}");
            Console.WriteLine($"  Id: {appSettings.ShowSettings.Id}");
            Console.WriteLine($"  Url: {appSettings.ShowSettings.Url}");
            Console.WriteLine($"  User: {appSettings.ShowSettings.User}");
            Console.WriteLine($"  Name: {appSettings.ShowSettings.Name}");
            Console.WriteLine($"  Login: {appSettings.ShowSettings.Login}");
            Console.WriteLine($"  Password: {appSettings.ShowSettings.Password}");
            Console.WriteLine($"  AllData: {appSettings.ShowSettings.AllData}");
            Console.WriteLine("  DataNames:");
            foreach (var name in appSettings.ShowSettings.DataNames)
            {
                Console.WriteLine($"    {name}");
            }
        }
    }
}