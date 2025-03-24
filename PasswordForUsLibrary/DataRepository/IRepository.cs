using PasswordForUsLibrary.Model;

namespace PasswordForUsLibrary.DataRepository;

public interface IRepository
{
    void AddNode(NodeDataModel node);
    void ImportNode(NodeDataModel node);
    IEnumerable<NodeDataModel> FindNode(SearchDataModel node);
    IEnumerable<NodeDataModel> GetAll();
    void Delete(int nodeId);
    void ChangeNode(NodeDataModel model);
    Guid GetStorageDataVersion();
    void SetVersion(Guid version, long ticks);
    long GetStorageDataChangeTime();
}