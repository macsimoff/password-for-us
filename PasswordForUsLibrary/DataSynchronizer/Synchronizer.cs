using PasswordForUsLibrary.DataRepository;
using PasswordForUsLibrary.Model;

namespace PasswordForUsLibrary.DataSynchronizer;

public abstract class Synchronizer(IRepository repo)
{
    protected readonly IRepository Repo = repo;

    public abstract void SynchronizeStorage(SynchronizationDataModel data);

    public SynchronizeData MergeData(SynchronizeData data1, SynchronizeData data2)
    {
        if (data1.Version == data2.Version)
            return Copy(data1);
        
        var mergedData = new List<NodeDataModel>();
        var mergedVersion = data1.ChangeTimeTicks >= data2.ChangeTimeTicks? data1.Version: data2.Version;
        
        var data1Dict = data1.Data.ToDictionary(d => d.Guid);
        var data2Dict = data2.Data.ToDictionary(d => d.Guid);
        
        foreach (var kvp in data1Dict)
        {
            var node1 = kvp.Value;
            var node = node1;
            if (data2Dict.TryGetValue(kvp.Key, out var node2))
            {
                if(!data1.Equals(data2))
                {
                    if (node1.ChangeTimeTicks == node2.ChangeTimeTicks) // conflict!!
                    {
                        mergedVersion = Guid.NewGuid();
                    }

                    node = MergeNode(node1, node2);
                    data2Dict.Remove(kvp.Key);
                }
            }
            else
            {
                if (mergedVersion == data2.Version)
                    mergedVersion = Guid.NewGuid();
            }
            
            mergedData.Add(node);
        }

        foreach (var kvp in data2Dict)
        {
            mergedData.Add(kvp.Value);
            if (mergedVersion == data1.Version)
                mergedVersion = Guid.NewGuid();
        }

        var ticks = GetMergedTicks(data1, data2, mergedVersion);
        return new SynchronizeData(mergedVersion, ticks, mergedData);
    }

    private static long GetMergedTicks(SynchronizeData data1, SynchronizeData data2, Guid mergedVersion)
    {
        var ticks = DateTime.Now.Ticks;
        if (mergedVersion == data1.Version)
            ticks = data1.ChangeTimeTicks;
        if (mergedVersion == data2.Version)
            ticks = data2.ChangeTimeTicks;
        return ticks;
    }

    public static SynchronizeData Copy(SynchronizeData data)
    {
        var copiedData = data.Data.Select(node => new NodeDataModel
        {
            Guid = node.Guid,
            ChangeTimeTicks = node.ChangeTimeTicks,
            Id = node.Id,
            User = node.User,
            Url = node.Url,
            Title = node.Title,
            Login = node.Login,
            Password = node.Password,
            Data = new Dictionary<string, string>(node.Data)
        }).ToList();

        return new SynchronizeData(data.Version, data.ChangeTimeTicks, copiedData);
    }

    public static NodeDataModel MergeNode(NodeDataModel node1, NodeDataModel node2)
    {
        var node = node1.ChangeTimeTicks >= node2.ChangeTimeTicks ? node1 : node2;
        
        var mergedNode = new NodeDataModel
        {
            Guid = node.Guid,
            ChangeTimeTicks = node.ChangeTimeTicks,
            Id = node.Id,
            User = node.User,
            Url = node.Url,
            Title = node.Title,
            Login = node.Login,
            Password = node.Password,
            Data = new Dictionary<string, string>(node.Data)
        };

        var oldNode = node1.ChangeTimeTicks >= node2.ChangeTimeTicks ? node2 : node1;
        SaveStory(mergedNode, oldNode);

        return mergedNode;
    }

    private static void SaveStory(NodeDataModel newNode, NodeDataModel oldNode)
    {
        var oldTime = new DateTime(oldNode.ChangeTimeTicks);
        var dataNameSuffix = oldTime.ToShortDateString() + "_" + oldNode.ChangeTimeTicks;

        if (oldNode.User != null && oldNode.User != newNode.User)
        {
            newNode.Data["User_" + dataNameSuffix] = oldNode.User;
        }

        if (oldNode.Title != null && oldNode.Title != newNode.Title)
        {
            newNode.Data["Title_"+dataNameSuffix] = oldNode.Title;
        }

        if (oldNode.Url != null && oldNode.Url != newNode.Url)
        {
            newNode.Data["Url_" + dataNameSuffix] = oldNode.Url;
        }

        if (oldNode.Login != null && oldNode.Login != newNode.Login)
        {
            newNode.Data["Login_" + dataNameSuffix] = oldNode.Login;
        }

        if (oldNode.Password != null && oldNode.Password != newNode.Password)
        {
            newNode.Data["Password_" + dataNameSuffix] = oldNode.Password;
        }

        foreach (var kvp in oldNode.Data)
        {
            if (newNode.Data.TryGetValue(kvp.Key, out var value) && value == kvp.Value)
            {
                continue;
            }

            newNode.Data["Data_"+ kvp.Key + "_" + dataNameSuffix] = kvp.Value;
        }
    }
}