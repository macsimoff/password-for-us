using FileStorage.FileReaders;
using FileStorage.WritersReaders;
using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;

namespace FileStorage;

public class Storage(string fileName) : IRepository, IDisposable
{
    private readonly JsonFileStore _fileStore = new(fileName);

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

    public async Task<IEnumerable<EncryptedData>> FindNodeAsync(SearchDataModel model)
    {
        
        var data = await _fileStore.ReadFileAsync();
        return data.Nodes.Where(n => model.ById && n.Id == model.Id
                        || model.ByUrl && n.Url.Contains(model.Url)
                        || model.ByName && n.Name.Contains(model.Name))
            .Select(ModelConvertor.Convert);
    }

    public async Task AddNodeAsync(EncryptedData model)
    {
        var data = await _fileStore.ReadFileAsync();
        model.Id = data.Nodes.Count();
        data.Nodes.Add(ModelConvertor.Create(model));
        await _fileStore.Write(data);
    }

    public void Dispose()
    {
        _fileStore.Dispose();
    }
}