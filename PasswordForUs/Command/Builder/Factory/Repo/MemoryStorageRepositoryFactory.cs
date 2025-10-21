using MemoryStorage;
using PasswordForUs.Abstractions;

namespace PasswordForUs.Command.Builder.Factory.Repo;

public class MemoryStorageRepositoryFactory: IRepositoryFactory
{
    public IRepository Create()
    {
        return new MemoryStorageRepository();
    }
}