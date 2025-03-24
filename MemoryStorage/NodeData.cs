namespace MemoryStorage;

internal class NodeData(
    Guid guid,
    long changeTimeTicks,
    int id,
    string? user,
    string? url,
    string? title,
    string? login,
    string? password,
    Dictionary<string, string> data)
{
    public Guid Guid { get; set; } = guid;
    public int Id { get; set; } = id;

    public string? User { get; set; } = user;
    public string? Url { get; set; } = url;
    public string? Title { get; set; } = title;
    public string? Login { get; set; } = login;
    public string? Password { get; set; } = password;
    public Dictionary<string, string> Data { get; set; } = data;
    public long ChangeTimeTicks { get; set; } = changeTimeTicks;
}