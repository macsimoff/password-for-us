using PasswordForUs.Abstractions.Models;

namespace PasswordForUs.Abstractions;

public interface IRepository
{
    void AddNode(NodeData node);
    void ImportNode(NodeData node);
    IEnumerable<NodeData> FindNode(SearchDataModel node);
    IEnumerable<NodeData> GetAll();
    void Delete(int nodeId);
    void ChangeNode(NodeData model);
    Guid GetStorageDataVersion();
    void SetVersion(Guid version, long ticks);
    long GetStorageDataChangeTime();

    Task<IEnumerable<NodeData>> FindNodeAsync(SearchDataModel node);
    Task AddNodeAsync(NodeData model);
}