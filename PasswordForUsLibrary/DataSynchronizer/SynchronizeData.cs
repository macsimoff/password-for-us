using PasswordForUs.Abstractions.Models;

namespace PasswordForUsLibrary.DataSynchronizer;

public class SynchronizeData(Guid version, long changeTimeTicks, List<NodeData> data)
{
    public Guid Version { get; } = version;
    public long ChangeTimeTicks { get; } = changeTimeTicks;
    public List<NodeData> Data { get; } = data;
}