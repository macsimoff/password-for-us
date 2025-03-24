using FileSynchronizer;
using MemoryStorage;
using PasswordForUs.Command.Builder.Factory.Repo;
using PasswordForUs.Command.Builder.Factory.Security;
using PasswordForUs.Command.Builder.Factory.Sync;
using PasswordForUs.Security;

namespace PasswordForUs.Command.Builder;

public class SynchronizeDataCommandBuilder(
    IRepositoryFactory repoFactory,
    ISynchronizerFactory syncFactory,
    ISecurityFactory securityFactory) : ICommandBuilder
{
    public ICommand Build(string[] commandData)
    {
        return BuildCommand();
    }

    private ICommand BuildCommand()
    {
        var repo = repoFactory.Create();
        var sync = syncFactory.Create(repo);
        var security = securityFactory.Create();
        return new SynchronizeDataCommand(sync, security);
    }
}