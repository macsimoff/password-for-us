using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.Json;
using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;

namespace Encryption.Aes;

public class AesEncryptionService: IEncryptionService
{
    private readonly IPasswordHashing _pHashing;
    private readonly int _pbkdf2Iterations;

    private const int SaltSize = 16;   // 128-bit salt
    private const int NonceSize = 12;  // 96-bit IV для AES-GCM
    private const int TagSize = 16;    // 128-bit auth tag

    public AesEncryptionService(
        IPasswordHashing pHashing,
        int pbkdf2Iterations = 200_000)
    {
        if (pbkdf2Iterations <= 0)
            throw new ArgumentOutOfRangeException(nameof(pbkdf2Iterations));

        _pHashing = pHashing;
        _pbkdf2Iterations = pbkdf2Iterations;
    }
    public NodeData Decrypt(byte[] masterKey,EncryptedData encryptedData)
    {
        Validate(masterKey);
        var decryptData = GetDecryptData(masterKey, encryptedData.Bytes);
        return new NodeData
        {
            Guid = encryptedData.Guid,
            Id = encryptedData.Id,
            Name = encryptedData.Name,
            Url = encryptedData.Url,
            User = encryptedData.User,
            Login = decryptData.Login,
            Password = decryptData.Password,
            Data = decryptData.Data
        };
    }

    public EncryptedData Encrypt(byte[] masterKey, NodeData data)
    {
        Validate(masterKey);
        return new EncryptedData()
        {
            Guid = data.Guid,
            Id = data.Id,
            Name = data.Name,
            Url = data.Url,
            User = data.User,
            Bytes = GetEncryptData(masterKey, data)
        };
    }

    private void Validate(byte[] masterKey)
    {
        if (masterKey is null || masterKey.Length == 0)
            throw new ArgumentException("Master key not set.", nameof(masterKey));
    }

    private DecryptData GetDecryptData(byte[] masterKey,byte[]? dataByte)
    {
        if (dataByte is null)
            return DecryptData.Empty;

        // encrypted data format: salt | iv | ciphertext | tag
        if (dataByte.Length < SaltSize + NonceSize + TagSize)
            throw new CryptographicException("Invalid encrypted data format.");
        
        var salt = new byte[SaltSize];
        Buffer.BlockCopy(dataByte, 0, salt, 0, SaltSize);

        var nonce = new byte[NonceSize];
        Buffer.BlockCopy(dataByte, SaltSize, nonce, 0, NonceSize);

        var cipherLen = dataByte.Length - SaltSize - NonceSize - TagSize;
        if (cipherLen <= 0)
            throw new CryptographicException("Invalid ciphertext size.");

        var ciphertext = new byte[cipherLen];
        Buffer.BlockCopy(dataByte, SaltSize + NonceSize, ciphertext, 0, cipherLen);

        var tag = new byte[TagSize];
        Buffer.BlockCopy(dataByte, dataByte.Length - TagSize, tag, 0, TagSize);

       var key = _pHashing.GetHash_256(masterKey, salt, _pbkdf2Iterations);

        var plaintext = new byte[cipherLen];
        using (var gcm = new AesGcm(key, tag.Length))
        {
            gcm.Decrypt(nonce, ciphertext, tag, plaintext);
        }

        
        return DecryptData.Serialise(plaintext);
    }

    private byte[] GetEncryptData(byte[] masterKey, NodeData data)
    {
        var payload = new DecryptData
        {
            Login = data.Login,
            Password = data.Password,
            Data = data.Data
        };

        var plaintext = payload.Serialize();

        var salt = new byte[SaltSize];
        RandomNumberGenerator.Fill(salt);

        var nonce = new byte[NonceSize];
        RandomNumberGenerator.Fill(nonce);

        var key = _pHashing.GetHash_256(masterKey, salt, _pbkdf2Iterations);

        var ciphertext = new byte[plaintext.Length];
        var tag = new byte[TagSize];

        using (var gcm = new AesGcm(key, tag.Length))
        {
            gcm.Encrypt(nonce, plaintext, ciphertext, tag);
        }

        // encrypted data format: salt | iv | ciphertext | tag
        var output = new byte[SaltSize + NonceSize + ciphertext.Length + TagSize];
        Buffer.BlockCopy(salt, 0, output, 0, SaltSize);
        Buffer.BlockCopy(nonce, 0, output, SaltSize, NonceSize);
        Buffer.BlockCopy(ciphertext, 0, output, SaltSize + NonceSize, ciphertext.Length);
        Buffer.BlockCopy(tag, 0, output, SaltSize + NonceSize + ciphertext.Length, TagSize);

        return output;
    }
}

internal class DecryptData
{
    public static DecryptData Empty => new() { Data = new Dictionary<string, string>() };
    public string? Login { get; init; }
    public string? Password { get; init; }
    public Dictionary<string, string> Data { get; init; }

    public static DecryptData Serialise(byte[] plaintext)
    {
        
        return JsonSerializer.Deserialize<DecryptData>(plaintext) ?? Empty;
        
    }

    public byte[] Serialize()
    {
        return JsonSerializer.SerializeToUtf8Bytes(this);
    }
}