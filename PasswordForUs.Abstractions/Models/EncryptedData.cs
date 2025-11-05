namespace PasswordForUs.Abstractions.Models;

public class EncryptedData
{
    public Guid Guid { get; set; }
    public long ChangeTimeTicks { get; set;}
    public int Id { get; set; }
    public string? User { get;set;} 
    public string? Url { get; set;} 
    public string? Name { get; set;}
    public byte[] Bytes { get; init; }
}