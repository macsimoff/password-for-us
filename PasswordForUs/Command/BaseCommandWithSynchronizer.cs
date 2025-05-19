using PasswordForUs.Security;
using PasswordForUs.Settings;
using PasswordForUsLibrary.DataSynchronizer;
using PasswordForUsLibrary.Model;

namespace PasswordForUs.Command;

public abstract class BaseCommandWithSynchronizer(Synchronizer synchronizer,ISecurity security) : ICommand
{
    protected void SynchronizeStorage(AppSettings settings)
    {
        SynchronizeStorage(settings.SyncDataPath, settings.Pass, settings.PassHashIteration);
    }
    
    
    protected void SynchronizeStorage(string path, string pass, int iteration)
    {
        var key =security.GenerateKey(pass, iteration);
        var iv = security.GetIV(pass, iteration);
        synchronizer.SynchronizeStorage(new SynchronizationDataModel()
        {
            FileName = path,
            Key = key,
            IV = iv
        });
        Console.WriteLine(Resources.Resources.Base_SynchronizationCompleted);
    }

    public abstract void Execute(AppSettings appSettings);
}