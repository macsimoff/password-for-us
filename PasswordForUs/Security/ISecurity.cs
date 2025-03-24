namespace PasswordForUs.Security;

public interface ISecurity
{
    public byte[] GenerateKey(string password, int iteration = 100500, string? salt = null);
    public byte[] GetIV(string password, int iteration = 100500, string? salt = null);
}