using PasswordForUsLibrary.DataRepository;
using PasswordForUsLibrary.Model;

namespace PasswordForUsLibrary.DataController;

public class SearchDataController
{
    private readonly IRepository _repository;

    public SearchDataController(IRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<NodeDataModel> Search(SearchDataModel searchModel)
    {
        return _repository.FindNode(searchModel);
    }
}