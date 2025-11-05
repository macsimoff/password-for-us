using System.Security.Cryptography;
using PasswordForUs.Abstractions;

namespace PasswordHashing;

public class Pbkdf2Hashing: IPasswordHashing
{
    private const int KeySize256 = 32;
    public byte[] GetHash_256(byte[] masterKey, byte[] salt, int ierations)
    {
        var kdf = new Rfc2898DeriveBytes(masterKey, salt, ierations, HashAlgorithmName.SHA256);
        return kdf.GetBytes(KeySize256);
    }
}