using PasswordForUsLibrary.DataRepository;

namespace PasswordForUs.Command.Builder.Factory.Repo;

public interface IRepositoryFactory
{
    IRepository Create();
}