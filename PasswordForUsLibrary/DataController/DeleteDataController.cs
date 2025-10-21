
using PasswordForUs.Abstractions;

namespace PasswordForUsLibrary.DataController;

public class DeleteDataController(IRepository repository)
{
    private readonly IRepository _repository = repository;

    public void DeletePassword(int dataId)
    {
        //_repository.Delete(dataId);
        throw new NotImplementedException("We have deliberately removed this functionality to avoid losing your data.");
    }
}