using System.Security.Cryptography;
using System.Text;

namespace PasswordForUs.Security;

public class Pbkdf2Security: ISecurity
{
    public byte[] GenerateKey(string password, int iteration = 100500, string? salt = null)
    {
        var saltedPassword = Encoding.UTF32.GetBytes(password);
        
        return Rfc2898DeriveBytes.Pbkdf2(saltedPassword, Encoding.UTF8.GetBytes(salt ?? ""), iteration,
            HashAlgorithmName.SHA256, 32);
    }

    public byte[] GetIV(string password, int iteration = 100500, string? salt = null)
    {
        var saltedPassword = Encoding.UTF32.GetBytes(password);
        
        return Rfc2898DeriveBytes.Pbkdf2(saltedPassword, Encoding.UTF8.GetBytes(salt ?? ""), iteration,
            HashAlgorithmName.SHA256, 16);
    }
}