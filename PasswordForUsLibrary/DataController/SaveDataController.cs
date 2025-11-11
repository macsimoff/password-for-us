using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;

namespace PasswordForUsLibrary.DataController;

public class SaveDataController : ISaveDataController
{
    private IRepository _repository;

    public SaveDataController(IRepository repo)
    {
        _repository = repo;
    }

    public void AddNewData(NodeData model)
    {
        _repository.AddNode(model);
    }

    public void ChangeData(NodeData model)
    {
        _repository.ChangeNode(model);
    }

    public Task CreateDataAsync(EncryptedData model)
    {
        return _repository.AddNodeAsync(model);
    }

    public Task CreateNodesAsync(IEnumerable<EncryptedData> models)
    {
        return _repository.AddNodesAsync(models);
    }
}