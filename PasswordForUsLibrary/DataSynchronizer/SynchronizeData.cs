using System.Security.Cryptography;
using PasswordForUsLibrary.Model;

namespace PasswordForUsLibrary.DataSynchronizer;

public class SynchronizeData(Guid version, long changeTimeTicks, List<NodeDataModel> data)
{
    public Guid Version { get; } = version;
    public long ChangeTimeTicks { get; } = changeTimeTicks;
    public List<NodeDataModel> Data { get; } = data;
}