using PasswordForUs.Security;
using PasswordForUs.Settings;
using PasswordForUsLibrary.DataSynchronizer;

namespace PasswordForUs.Command;

public class SynchronizeDataCommand(Synchronizer synchronizer,ISecurity security) 
    : BaseCommandWithSynchronizer(synchronizer, security)
{
    public override void Execute(AppSettings appSettings)
    {
        SynchronizeStorage(appSettings);
    }
}