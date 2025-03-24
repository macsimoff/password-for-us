namespace PasswordForUsLibrary.Model;

public class NodeDataModel(Guid guid, long changeTimeTicks, int id, string? user, string? title, string? url, string? login, string? password)
{
    public Guid Guid { get; set; } = guid;
    public long ChangeTimeTicks { get; set;} = changeTimeTicks;
    public int Id { get; set; } = id;
    public string? User { get;set;} = user;
    public string? Url { get; set;} = url;
    public string? Title { get; set;} = title;
    public string? Login { get; set;} = login;
    public string? Password { get; set;} = password;
    public Dictionary<string, string> Data { get; set;} = new(0);

    public NodeDataModel(): this(Guid.Empty,0, -1,"","","","","")
    {
    }

    public NodeDataModel(string? user, string? title, string? url, string? login, string? password)
        : this(Guid.NewGuid(), DateTime.Now.Ticks,-1, user, title, url, login, password)
    {
    }

    public NodeDataModel(Guid guid, long changeTimeTicks, int id,string? user, string? url, string? title, string? login, string? password, Dictionary<string, string> data)
        : this (guid, changeTimeTicks,id, user, title, url, login, password)
    {
        Data = data;
    }

    public override bool Equals(object? obj)
    {
        if (obj is NodeDataModel other)
        {
            return this.Equals(other);
        }
        return false;
    }

    protected bool Equals(NodeDataModel other)
    {
        return Guid.Equals(other.Guid) 
               && ChangeTimeTicks == other.ChangeTimeTicks 
               && Id == other.Id 
               && User == other.User 
               && Url == other.Url 
               && Title == other.Title 
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
        hashCode.Add(Title);
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