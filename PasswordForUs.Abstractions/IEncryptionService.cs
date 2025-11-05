using PasswordForUs.Abstractions.Models;

namespace PasswordForUs.Abstractions;

public interface IEncryptionService
{
    NodeData Decrypt (byte[] masterKey, EncryptedData encryptedData);
    EncryptedData Encrypt(byte[] masterKey, NodeData data);
}