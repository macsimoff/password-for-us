using PasswordForUsLibrary.DataRepository;
using PasswordForUsLibrary.Model;

namespace MemoryStorage;

public class MemoryStorageRepository: IRepository
{
    public void AddNode(NodeDataModel node)
    {
        var data = ModelConvertor.Convert(node);
        var lastKey = Storage.Data.Keys.Count > 0? Storage.Data.Keys.Last() : -1;
        data.Id = lastKey + 1;
        Storage.Data.Add(data.Id, data);
        SetNewStorageVersion();
    }

    public void ImportNode(NodeDataModel node)
    {
        var data = ModelConvertor.Convert(node);
        Storage.Data.Add(data.Id, data);
    }

    public IEnumerable<NodeDataModel> FindNode(SearchDataModel model)
    {
        if (Storage.Data.Count == 0)
        {
            throw new InvalidOperationException("There are no nodes in the storage.");
        }

        var result = new List<NodeDataModel>();
        if (model.ById)
        {
            result.AddRange(Storage.Data.TryGetValue(model.Id, out var value)? 
                new List<NodeDataModel> {ModelConvertor.Convert(value)} : new List<NodeDataModel>()
            );
        }

        result.AddRange(Storage.Data.Where(x => 
                   model.ByName && x.Value.Title != null && x.Value.Title.ToLower().Contains(model.Name.ToLower()) 
                || model.ByUrl && x.Value.Url!= null && x.Value.Url.Contains(model.Url.ToLower()))
            .Select(x =>ModelConvertor.Convert(x.Value))
        );

        return result;
    }

    public IEnumerable<NodeDataModel> GetAll()
    {
        return Storage.Data.Select(x =>ModelConvertor.Convert(x.Value));
    }

    public void Delete(int nodeId)
    {
        throw new NotImplementedException();
        //todo: Реализовать дархив для таких записей и созможность востанавливать их.
    }

    public void ChangeNode(NodeDataModel model)
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