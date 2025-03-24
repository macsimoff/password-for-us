using PasswordForUs.Model;
using PasswordForUs.Security;
using PasswordForUs.Settings;
using PasswordForUsLibrary.DataController;
using PasswordForUsLibrary.DataSynchronizer;
using PasswordForUsLibrary.Model;
using PasswordForUsLibrary.PassGenerator;

namespace PasswordForUs.Command;

public class AddCommand(
    PassCommandData data,
    SaveDataController controller,
    PassGenerator generator,
    Synchronizer synchronizer,
    ISecurity security)
    : BaseCommandWithSynchronizer(synchronizer, security)
{
    public override void Execute(AppSettings appSettings)
    {
        var model = GetModel(appSettings);
        controller.AddNewPassword(model);
        SynchronizeStorage(appSettings);
       Console.WriteLine("Password was added.");
    }

    private NodeDataModel GetModel(AppSettings appSettings)
    {
        if (string.IsNullOrEmpty(data.Url))
        {
            data.Url = GetUrl();
        }

        return new NodeDataModel(
            Guid.NewGuid(),
            DateTime.Now.Ticks,
            0, 
            string.IsNullOrEmpty(data.User)? "": data.User, //todo: get User from appSettings
            data.Url,
            string.IsNullOrEmpty(data.Title)? data.Url: data.Title,
            string.IsNullOrEmpty(data.Login)? GetLogin(): data.Login,
            string.IsNullOrEmpty(data.Pass)? GetPass(appSettings): data.Pass,
            data.Data ?? GetAdditionalDate());
    }

    private string GetLogin()
    {
        Console.WriteLine("Enter Login:");
        return Console.ReadLine() ?? "";
    }

    private Dictionary<string, string> GetAdditionalDate()
    {
        Console.WriteLine("""
                          Enter additional data 
                          format --<key> <value> 
                          write 'end' or empty string for finish:
                          """);
        var additionalDate = new Dictionary<string, string>();
        while (true)
        {
            var s = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(s) || s == "end")
            {
                break;
            }

            var splitString = s.Split(' ');
            foreach (var s1 in splitString)
            {
                if (s1.StartsWith("--"))
                {
                    additionalDate[s1.TrimStart('-')] = "";
                }
                else
                {
                    additionalDate[additionalDate.Keys.Last()] = $"{additionalDate[additionalDate.Keys.Last()]} {s1}";
                }
            }
        }

        return additionalDate;
    }

    private string GetPass(AppSettings appSettings)
    {
        Console.WriteLine("enter PASSWORD or enter Y/y to generate password automate:");
        var s = Console.ReadLine();
        return s == null || s.ToLower() == "y" ? generator.Generate(appSettings.DefaultPassLength, appSettings.CharacterSets) : s;
    }

    private string GetUrl()
    {
        Console.WriteLine("enter URL address:");
        return Console.ReadLine() ?? "";
    }
}