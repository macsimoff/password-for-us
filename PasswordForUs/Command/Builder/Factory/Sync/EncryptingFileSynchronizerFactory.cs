using FileSynchronizer;
using PasswordForUsLibrary.DataRepository;
using PasswordForUsLibrary.DataSynchronizer;

namespace PasswordForUs.Command.Builder.Factory.Sync;

public class EncryptingFileSynchronizerFactory: ISynchronizerFactory
{
    public Synchronizer Create(IRepository repo)
    {
        return new EncryptingFileSynchronizer(repo);
    }
}