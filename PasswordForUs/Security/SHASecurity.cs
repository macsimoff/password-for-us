using System.Security.Cryptography;
using System.Text;

namespace PasswordForUs.Security;

public class ShaSecurity: ISecurity
{
    public byte[] GenerateKey(string password, int iteration = 100500, string? salt = null)
    {
        var saltedPassword = GetSaltedPassword(password, salt);
        byte[] hash = saltedPassword;

        using (SHA512 sha512 = SHA512.Create())
        {
            for (long i = 0; i < iteration; i++)
            {
                //todo: чуть чуть поменять hash перед новой генерацией чтобы уменьшить его дигродацию
                hash = sha512.ComputeHash(hash);
            }
        }

        var key = new byte[32]; // AES-256 requires a 32-byte key
        Array.Copy(hash, key, key.Length);
        return key;
    }

    public byte[] GetIV(string password, int iteration = 100500, string? salt = null)
    {
        var saltedPassword = GetSaltedPassword(password, salt);
        var hash = saltedPassword;

        using (var sha512 = SHA512.Create())
        {
            for (long i = 0; i < iteration; i++)
            {
                hash = sha512.ComputeHash(hash);
            }
        }
        
        var key = new byte[16]; // AES-256 requires a 16-byte IV
        // copy last 16 bytes from hash to key
        Array.Copy(hash, hash.Length - 16, key, 0, key.Length);
        return key;
    }
    
    private static byte[] GetSaltedPassword(string password, string? salt)
    {
        var saltedPassword = new List<byte>();
        saltedPassword.AddRange(Encoding.UTF32.GetBytes(password));
        if (!string.IsNullOrEmpty(salt)) saltedPassword.AddRange(Encoding.UTF32.GetBytes(salt));
        return saltedPassword.ToArray();
    }
}