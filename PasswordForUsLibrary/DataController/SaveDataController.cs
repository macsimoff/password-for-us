using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;

namespace PasswordForUsLibrary.DataController;

public class SaveDataController
{
    private IRepository _repository;

    public SaveDataController(IRepository repo)
    {
        _repository = repo;
    }

    public void AddNewPassword(NodeData model)
    {
        _repository.AddNode(model);
    }
    
    public void ChangePassword(NodeData model)
    {
        _repository.ChangeNode(model);
    }
}