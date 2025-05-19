using PasswordForUs.Model;
using PasswordForUs.Security;
using PasswordForUs.Settings;
using PasswordForUsLibrary.DataController;
using PasswordForUsLibrary.DataSynchronizer;
using PasswordForUsLibrary.Model;

namespace PasswordForUs.Command;

public class ChangePassCommand(
    PassCommandData commandData,
    SaveDataController saveController,
    SearchDataController searchController,
    Synchronizer synchronizer,
    ISecurity security)
    : BaseCommandWithSynchronizer(synchronizer, security)
{
    public override void Execute(AppSettings appSettings)
    {
        var list = searchController.Search(new SearchDataModel(commandData.Id)).ToList();
        if(list.Count > 1)
        {
            Console.WriteLine(Resources.Resources.ChangePass_ThereAreMoreThanOne);
            return;
        }
        if(list.Count == 0)
        {
            Console.WriteLine(Resources.Resources.ChangePass_ThereIsNoEntry);
            return;
        }

        var data = MergeData(list[0]);
        saveController.ChangePassword(data);
        SynchronizeStorage(appSettings);
        Console.WriteLine(Resources.Resources.ChangePass_PasswordChanged);
    }

    private NodeDataModel MergeData(NodeDataModel data)
    {
        if (commandData.User != null)
        {
            data.User = commandData.User;
        }

        if (commandData.Url != null)
        {
            data.Url = commandData.Url;
        }

        if (commandData.Title != null)
        {
            data.Title = commandData.Title;
        }

        if (commandData.Login != null)
        {
            data.Login = commandData.Login;
        }

        if (commandData.Pass != null)
        {
            data.Password = commandData.Pass;
        }

        if (commandData.Data != null)
        {
            foreach (var key in commandData.Data.Keys)
            {
                data.Data[key] = commandData.Data[key];
            }
        }

        return data;
    }
}