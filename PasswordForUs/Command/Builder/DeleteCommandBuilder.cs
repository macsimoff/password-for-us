using FileSynchronizer;
using MemoryStorage;
using PasswordForUs.Command.Builder.Factory.Repo;
using PasswordForUs.Command.Builder.Factory.Security;
using PasswordForUs.Command.Builder.Factory.Sync;
using PasswordForUs.Model;
using PasswordForUsLibrary.DataController;

namespace PasswordForUs.Command.Builder;

public class DeleteCommandBuilder(
    IRepositoryFactory repoFactory,
    ISynchronizerFactory syncFactory,
    ISecurityFactory securityFactory) : ICommandBuilder
{
    public ICommand Build(string[] commandData)
    {
        var data = GetCommandData(commandData);
        return BuildCommand(data);
    }

    private ICommand BuildCommand(DeleteCommandData data)
    {
        var repo = repoFactory.Create();
        var controller = new DeleteDataController(repo);
        var sync = syncFactory.Create(repo);
        var security = securityFactory.Create();
        return new DeleteCommand(data, controller, sync, security);
    }

    private DeleteCommandData GetCommandData(string[] commandData)
    {
        var id = 0;
        for (var i = 0; i < commandData.Length; i++)
        {
            if (commandData[i] == "--id" || commandData[i] == "-i")
            {
                try
                {
                    id = int.Parse(commandData[i + 1]);
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Invalid id value");
                }
            }
        }

        return new DeleteCommandData() { Id = id };
    }
}