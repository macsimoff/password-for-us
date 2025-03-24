using PasswordForUsLibrary.DataRepository;
using PasswordForUsLibrary.Model;

namespace PasswordForUsLibrary.DataController;

public class SaveDataController
{
    private IRepository _repository;

    public SaveDataController(IRepository repo)
    {
        _repository = repo;
    }

    public void AddNewPassword(NodeDataModel model)
    {
        _repository.AddNode(model);
    }
    
    public void ChangePassword(NodeDataModel model)
    {
        _repository.ChangeNode(model);
    }
}