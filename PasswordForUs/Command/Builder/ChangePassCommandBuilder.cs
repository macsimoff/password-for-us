using FileSynchronizer;
using MemoryStorage;
using PasswordForUs.Command.Builder.DataBuilder;
using PasswordForUs.Command.Builder.Factory.Repo;
using PasswordForUs.Command.Builder.Factory.Security;
using PasswordForUs.Command.Builder.Factory.Sync;
using PasswordForUs.Model;
using PasswordForUsLibrary.DataController;

namespace PasswordForUs.Command.Builder;

public class ChangePassCommandBuilder(
    IRepositoryFactory repoFactory,
    ISynchronizerFactory syncFactory,
    ISecurityFactory securityFactory) : ICommandBuilder
{
    private readonly PassCommandDataBuilder _dataBuilder = new();

    public ICommand Build(string[] commandData)
    {
        var data = _dataBuilder.CreateCommandData(commandData);
        return BuildCommand(data);
    }

    private ICommand BuildCommand(PassCommandData data)
    {
        var repo = repoFactory.Create();
        var saveController = new SaveDataController(repo);
        var searchController = new SearchDataController(repo);
        var sync = syncFactory.Create(repo);
        var security = securityFactory.Create();
        return new ChangePassCommand(data, saveController, searchController, sync, security);
    }
}