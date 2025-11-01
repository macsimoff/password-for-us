namespace PasswordForUs.Abstractions.Models;

public class NodeData(Guid guid, long changeTimeTicks, int id, string? user, string? name, string? url, string? login, string? password)
{
    public Guid Guid { get; set; } = guid;
    public long ChangeTimeTicks { get; set;} = changeTimeTicks;
    public int Id { get; set; } = id;
    public string? User { get;set;} = user;
    public string? Url { get; set;} = url;
    public string? Name { get; set;} = name;
    public string? Login { get; set;} = login;
    public string? Password { get; set;} = password;
    public Dictionary<string, string> Data { get; set;} = new(0);

    public NodeData(): this(Guid.Empty,0, -1,"","","","","")
    {
    }

    public NodeData(string? user, string? name, string? url, string? login, string? password)
        : this(Guid.NewGuid(), DateTime.Now.Ticks,-1, user, name, url, login, password)
    {
    }

    public NodeData(Guid guid, long changeTimeTicks, int id,string? user, string? url, string? name, string? login, string? password, Dictionary<string, string> data)
        : this (guid, changeTimeTicks,id, user, name, url, login, password)
    {
        Data = data;
    }

    public override bool Equals(object? obj)
    {
        if (obj is NodeData other)
        {
            return this.Equals(other);
        }
        return false;
    }

    protected bool Equals(NodeData other)
    {
        return Guid.Equals(other.Guid) 
               && ChangeTimeTicks == other.ChangeTimeTicks 
               && Id == other.Id 
               && User == other.User 
               && Url == other.Url 
               && Name == other.Name 
               && Login == other.Login 
               && Password == other.Password 
               && Data.Equals(other.Data);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Guid);
        hashCode.Add(ChangeTimeTicks);
        hashCode.Add(Id);
        hashCode.Add(User);
        hashCode.Add(Url);
        hashCode.Add(Name);
        hashCode.Add(Login);
        hashCode.Add(Password);
        foreach (var kvp in Data)
        {
            hashCode.Add(kvp.Key);
            hashCode.Add(kvp.Value);
        }
        return hashCode.ToHashCode();
    }
}