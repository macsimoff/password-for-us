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
                ChangeTimeTicks = 1234,
                Id = 1,
                User = "Я",
                Url = "HTTP://localhost",
                Title = "НАЗВАНИЕ",
                Login = "я-Login",
                Password = "124хъ!№;ээ",
                Data = null
            },
            new NodeData
            {
                Guid = Guid.Empty,
                ChangeTimeTicks = 2345,
                Id = 2,
                User = "Он",
                Url = "https://translate.google.com/",
                Title = "google",
                Login = "google-Login",
                Password = "фы234asd!№;ээ",
                Data = null
            }
        ];
    }
}