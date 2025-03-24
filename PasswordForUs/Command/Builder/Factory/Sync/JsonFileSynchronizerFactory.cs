using FileSynchronizer;
using PasswordForUsLibrary.DataRepository;
using PasswordForUsLibrary.DataSynchronizer;

namespace PasswordForUs.Command.Builder.Factory.Sync;

public class JsonFileSynchronizerFactory: ISynchronizerFactory
{
    public Synchronizer Create(IRepository repo)
    {
        return new JsonFileSynchronizer(repo);
    }
}