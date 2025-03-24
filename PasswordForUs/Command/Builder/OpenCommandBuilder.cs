using MemoryStorage;
using PasswordForUs.Command.Builder.Factory.Repo;
using PasswordForUs.Command.Builder.Factory.Security;
using PasswordForUs.Command.Builder.Factory.Sync;
using PasswordForUs.Model;

namespace PasswordForUs.Command.Builder;

public class OpenCommandBuilder(
    IRepositoryFactory repoFactory,
    ISynchronizerFactory syncFactory,
    ISecurityFactory securityFactory) : ICommandBuilder
{
    public ICommand Build(string[] commandData)
    {
        var data = CreateOpenCommandData(commandData);
        return BuildOpenCommand(data);
    }

    private OpenCommandData CreateOpenCommandData(string[] commandData)
    {
        return commandData.Length > 0 ? new OpenCommandData(commandData[0]) : new OpenCommandData(string.Empty);
    }

    private ICommand BuildOpenCommand(OpenCommandData data)
    {
        var repo = repoFactory.Create();
        var sync = syncFactory.Create(repo);
        var security = securityFactory.Create();
        return new OpenCommand(data, sync, security);
    }
}