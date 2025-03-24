using MemoryStorage;
using PasswordForUs.Command.Builder.DataBuilder;
using PasswordForUs.Command.Builder.Factory.Repo;
using PasswordForUs.Model;
using PasswordForUsLibrary.DataController;

namespace PasswordForUs.Command.Builder;

public class FindCommandBuilder(IRepositoryFactory repoFactory): ICommandBuilder
{
    private readonly FindCommandDataBuilder _dataBuilder = new ();
    public ICommand Build(string[] commandData)
    {
        var data = _dataBuilder.Build(commandData);
        return BuildFindCommand(data);
    }

    private ICommand BuildFindCommand(FindCommandData data)
    {
        var repo = repoFactory.Create();
        return new FindCommand(data, new SearchDataController(repo), new NodeWriter());
    }
}