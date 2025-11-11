using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;

namespace MemoryStorage;

public class MemoryStorageRepository: IRepository
{
    public void AddNode(PasswordForUs.Abstractions.Models.NodeData node)
    {
        var data = ModelConvertor.Convert(node);
        var lastKey = Storage.Data.Keys.Count > 0? Storage.Data.Keys.Last() : -1;
        data.Id = lastKey + 1;
        Storage.Data.Add(data.Id, data);
        SetNewStorageVersion();
    }

    public void ImportNode(PasswordForUs.Abstractions.Models.NodeData node)
    {
        var data = ModelConvertor.Convert(node);
        Storage.Data.Add(data.Id, data);
    }

    public IEnumerable<PasswordForUs.Abstractions.Models.NodeData> FindNode(SearchDataModel model)
    {
        if (Storage.Data.Count == 0)
        {
            throw new InvalidOperationException("There are no nodes in the storage.");
        }

        var result = new List<PasswordForUs.Abstractions.Models.NodeData>();
        if (model.ById)
        {
            result.AddRange(Storage.Data.TryGetValue(model.Id, out var value)? 
                new List<PasswordForUs.Abstractions.Models.NodeData> {ModelConvertor.Convert(value)} 
                : new List<PasswordForUs.Abstractions.Models.NodeData>()
            );
        }

        result.AddRange(Storage.Data.Where(x => 
                   model.ByName && x.Value.Title != null && x.Value.Title.ToLower().Contains(model.Name.ToLower()) 
                || model.ByUrl && x.Value.Url!= null && x.Value.Url.Contains(model.Url.ToLower()))
            .Select(x =>ModelConvertor.Convert(x.Value))
        );

        return result;
    }

    public IEnumerable<PasswordForUs.Abstractions.Models.NodeData> GetAll()
    {
        return Storage.Data.Select(x =>ModelConvertor.Convert(x.Value));
    }

    public void Delete(int nodeId)
    {
        throw new NotImplementedException();
        //todo: Реализовать дархив для таких записей и созможность востанавливать их.
    }

    public void ChangeNode(PasswordForUs.Abstractions.Models.NodeData model)
    {
        Storage.Data[model.Id] = ModelConvertor.Convert(model);
        SetNewStorageVersion();
    }

    public Guid GetStorageDataVersion()
    {
        return Storage.Version;
    }

    public long GetStorageDataChangeTime()
    {
        return Storage.ChangeTimeTicks;
    }

    public Task<IEnumerable<PasswordForUs.Abstractions.Models.EncryptedData>> FindNodeAsync(SearchDataModel node)
    {
        throw new NotImplementedException();
    }

    public Task AddNodeAsync(EncryptedData model)
    {
        throw new NotImplementedException();
    }

    public Task AddNodesAsync(IEnumerable<EncryptedData> models)
    {
        throw new NotImplementedException();
    }

    public Task AddNodeAsync(PasswordForUs.Abstractions.Models.NodeData model)
    {
        throw new NotImplementedException();
    }

    public void SetVersion(Guid version, long ticks)
    {
        Storage.Version = version;
        Storage.ChangeTimeTicks = ticks;
    }

    private static void SetNewStorageVersion()
    {
        Storage.Version = Guid.NewGuid();
        Storage.ChangeTimeTicks = DateTime.Now.Ticks;
    }
}