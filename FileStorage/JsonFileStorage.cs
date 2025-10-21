using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;

namespace FileStorage;

public class JsonFileStorage: IRepository
{
    public void AddNode(NodeData node)
    {
        throw new NotImplementedException();
    }

    public void ImportNode(NodeData node)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<NodeData> FindNode(SearchDataModel node)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<NodeData> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Delete(int nodeId)
    {
        throw new NotImplementedException();
    }

    public void ChangeNode(NodeData model)
    {
        throw new NotImplementedException();
    }

    public Guid GetStorageDataVersion()
    {
        throw new NotImplementedException();
    }

    public void SetVersion(Guid version, long ticks)
    {
        throw new NotImplementedException();
    }

    public long GetStorageDataChangeTime()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<NodeData>> FindNodeAsync(SearchDataModel node)
    {
        await Task.Delay(1000);
        return [
            new NodeData
            {
                Guid = Guid.Empty,
                ChangeTimeTicks = 0,
                Id = 0,
                User = "User",
                Url = "HTTP://localhost",
                Title = "зАГОЛОВОК",
                Login = "Login",
                Password = "ОченьСекретныйПароль",
                Data = null
            }
        ];
    }
}