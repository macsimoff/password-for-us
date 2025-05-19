using PasswordForUs.Model;
using PasswordForUs.Security;
using PasswordForUs.Settings;
using PasswordForUsLibrary.DataController;
using PasswordForUsLibrary.DataSynchronizer;

namespace PasswordForUs.Command;

public class DeleteCommand(
    DeleteCommandData commandData,
    DeleteDataController controller,
    Synchronizer synchronizer,
    ISecurity security)
    : BaseCommandWithSynchronizer(synchronizer, security)
{
    public override void Execute(AppSettings appSettings)
    {
        controller.DeletePassword(commandData.Id);
        SynchronizeStorage(appSettings);
        Console.WriteLine(Resources.Resources.Delete_PasswordDeleted);
    }
}