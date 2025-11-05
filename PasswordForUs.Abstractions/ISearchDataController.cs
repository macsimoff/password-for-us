using PasswordForUs.Abstractions.Models;

namespace PasswordForUs.Abstractions;

public interface ISearchDataController
{
    IEnumerable<NodeData> Search(SearchDataModel searchModel);
    Task<IEnumerable<EncryptedData>> SearchAsync(SearchDataModel searchModel);
}