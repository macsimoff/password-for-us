namespace PasswordForUs.Model;

public class PassCommandData
{
    public int Id { get; set; } = -1;
    public string? User { get; set; }
    public string? Url { get; set; }
    public string? Title { get; set; }
    public string? Pass { get; set; }
    public string? Login { get; set; }
    public Dictionary<string, string>? Data { get; set; }
    

    public void SetDataValue(string key, string s)
    {
        Data ??= new Dictionary<string, string>();
        
        Data[key] = s;
    }

    public string GetDataValue(string key)
    {
        if(Data == null) return string.Empty;
        return Data.TryGetValue(key, out var value) ? value : string.Empty;
    }
}