using FileStorage.FileReaders;
using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;

namespace FileStorage;

public class Storage(string fileName): IRepository
{
    private readonly IFileReader _reader = new JsonFileReader(fileName);
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

    public async Task<IEnumerable<NodeData>> FindNodeAsync(SearchDataModel model)
    {
        
        var data = await _reader.ReadFileAsync();
        return data.Nodes.Where(n => model.ById && n.Id == model.Id
                        || model.ByUrl && n.Url == model.Url
                        || model.ByName && n.Name == model.Name)
            .Select(ModelConvertor.Convert);
    }
}