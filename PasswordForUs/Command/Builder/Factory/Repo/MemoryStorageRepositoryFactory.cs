using MemoryStorage;
using PasswordForUsLibrary.DataRepository;

namespace PasswordForUs.Command.Builder.Factory.Repo;

public class MemoryStorageRepositoryFactory: IRepositoryFactory
{
    public IRepository Create()
    {
        return new MemoryStorageRepository();
    }
}