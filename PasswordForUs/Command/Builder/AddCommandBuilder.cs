using FileSynchronizer;
using MemoryStorage;
using PasswordForUs.Command.Builder.DataBuilder;
using PasswordForUs.Command.Builder.Factory.Repo;
using PasswordForUs.Command.Builder.Factory.Security;
using PasswordForUs.Command.Builder.Factory.Sync;
using PasswordForUs.Model;
using PasswordForUsLibrary.DataController;
using PasswordForUsLibrary.PassGenerator;

namespace PasswordForUs.Command.Builder;

public class AddCommandBuilder(
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
        var controller = new SaveDataController(repo);
        var sync = syncFactory.Create(repo);
        var security = securityFactory.Create();
        return new AddCommand(data, controller, new PassGenerator(), sync,security);
    }
}