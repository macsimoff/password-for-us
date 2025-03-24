using PasswordForUs.Model;
using PasswordForUs.Security;
using PasswordForUs.Settings;
using PasswordForUsLibrary.DataSynchronizer;
using PasswordForUsLibrary.Import;
using PasswordForUsLibrary.Model;

namespace PasswordForUs.Command;

public class OpenCommand(OpenCommandData commandData, Synchronizer synchronizer, ISecurity security) 
    : BaseCommandWithSynchronizer(synchronizer, security)
{
    public override void Execute(AppSettings appSettings)
    {
        if (!string.IsNullOrEmpty(commandData.Path))
            SynchronizeStorage(commandData.Path, appSettings.Pass, appSettings.PassHashIteration);
        else
            SynchronizeStorage(appSettings);
        Console.WriteLine("The password file has been opened.");
    }
}