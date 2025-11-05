namespace PasswordForUs.Abstractions;

public interface IPasswordHashing
{
    public byte[] GetHash_256(byte[] masterKey, byte[] salt, int pbkdf2Iterations);
}