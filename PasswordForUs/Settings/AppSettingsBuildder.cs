using System.Text.Json;

namespace PasswordForUs.Settings;

public static class AppSettingsBuilder
{
    public static AppSettings Build(string filePath)
    {
        try
        {
            var json = File.ReadAllText(filePath);
            var res = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            return res;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading settings: {ex.Message}. Using default settings.");
            return new AppSettings();
        }
    }
}