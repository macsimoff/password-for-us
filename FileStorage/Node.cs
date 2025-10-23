namespace FileStorage;

public class Node
{
    public Guid Guid { get; set; }
    public int Id { get; set; }
    public string Url { get; set; }
    public string Name { get; set; }
    public string? User { get; set; }
    public long ChangeTimeTicks { get; set; }
}