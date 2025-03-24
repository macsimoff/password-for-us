namespace MemoryStorage;

internal static class Storage
{
    public static Dictionary<int,NodeData> Data { get; } = new();
    public static Guid Version { get; set; }
    public static long ChangeTimeTicks { get; set; }
}