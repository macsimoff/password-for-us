using System.Security.Cryptography;
using Encryption.Aes;
using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;

namespace TestsForPasswordForUs.Encryption;

[TestFixture]
public class AesEncryptionServiceTest
{
    private static AesEncryptionService CreateService(int iterations = 10) => new(new TestPasswordHashing(), iterations);

    private static byte[] NewKey(int len = 32)
    {
        var key = new byte[len];
        RandomNumberGenerator.Fill(key);
        return key;
    }

    [Test]
    public void EncryptDecrypt_Roundtrip_Ok()
    {
        var service = CreateService();
        var masterKey = NewKey();
        var node = new NodeData
        {
            Login = "user1",
            Password = "p@ss",
            Data = new Dictionary<string, string> { ["note"] = "hello" }
        };

        var enc = service.Encrypt(masterKey, node);
        Assert.That(enc.Bytes, Is.Not.Null.And.Not.Empty);

        var dec = service.Decrypt(masterKey, enc);

        Assert.That(dec.Login, Is.EqualTo(node.Login));
        Assert.That(dec.Password, Is.EqualTo(node.Password));
        Assert.That(dec.Data, Is.EquivalentTo(node.Data));
    }

    [Test]
    public void Encrypt_SameInput_ProducesDifferentCiphertext()
    {
        var service = CreateService();
        var masterKey = NewKey();
        var node = new NodeData
        {
            Login = "user",
            Password = "pwd",
            Data = new Dictionary<string, string> { ["k"] = "v" }
        };

        var enc1 = service.Encrypt(masterKey, node);
        var enc2 = service.Encrypt(masterKey, node);

        Assert.That(enc1.Bytes, Is.Not.EqualTo(enc2.Bytes));
    }

    [Test]
    public void Encrypt_EmptyMasterKey_Throws()
    {
        var service = CreateService();
        var node = new NodeData { Login = "u", Password = "p", Data = new Dictionary<string, string>() };

        Assert.Throws<ArgumentException>(() => service.Encrypt(Array.Empty<byte>(), node));
        Assert.Throws<ArgumentException>(() => service.Encrypt(null!, node));
    }

    [Test]
    public void Decrypt_EmptyMasterKey_Throws()
    {
        var service = CreateService();
        var enc = new EncryptedData { Bytes = new byte[48] };

        Assert.Throws<ArgumentException>(() => service.Decrypt(Array.Empty<byte>(), enc));
        Assert.Throws<ArgumentException>(() => service.Decrypt(null!, enc));
    }

    [Test]
    public void Decrypt_InvalidFormat_Throws()
    {
        var service = CreateService();
        var masterKey = NewKey();
        var enc = new EncryptedData { Bytes = new byte[10] };

        Assert.Throws<CryptographicException>(() => service.Decrypt(masterKey, enc));
    }

    [Test]
    public void Decrypt_WrongMasterKey_Throws()
    {
        var service = CreateService();
        var key1 = NewKey();
        var key2 = NewKey();
        var node = new NodeData
        {
            Login = "u",
            Password = "p",
            Data = new Dictionary<string, string> { ["a"] = "b" }
        };

        var enc = service.Encrypt(key1, node);

        Assert.Throws<AuthenticationTagMismatchException>(() => service.Decrypt(key2, enc));
    }

    [Test]
    public void Ctor_NonPositiveIterations_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new AesEncryptionService(new TestPasswordHashing(), 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new AesEncryptionService(new TestPasswordHashing(), -1));
    }

    private sealed class TestPasswordHashing : IPasswordHashing
    {
        public byte[] GetHash_256(byte[] password, byte[] salt, int iterations)
        {
            return Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, 32);
        }
    }
}