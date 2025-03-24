using System.Text.Json;
using PasswordForUsLibrary.DataRepository;
using PasswordForUsLibrary.DataSynchronizer;
using PasswordForUsLibrary.Model;
using System.Security.Cryptography;
using PasswordForUsLibrary.Exception;

namespace FileSynchronizer;

public class EncryptingFileSynchronizer(IRepository repo) : FileSynchronizerBase(repo)
{
    protected override void ValidateDataModel(SynchronizationDataModel data)
    {
        if (data.Key == null || data.Key.Length != 32) // AES-256 key length
            throw new ArgumentException("Key must be 32 bytes long.");
        if (data.IV == null || data.IV.Length != 16) // AES block size
            throw new ArgumentException("IV must be 16 bytes long.");
    }

    protected override  SynchronizeData LoadRemoteStorage(SynchronizationDataModel model)
    {
        using var fs = new FileStream(model.FileName, FileMode.Open, FileAccess.Read);
        var buffer = new byte[fs.Length];
        var result = fs.Read(buffer, 0, buffer.Length);
        
        if(result != buffer.Length)
            throw new ArgumentException($"Problem with file {model.FileName}. Cannot read all content.");
        
        var emptyData = new SynchronizeData(Guid.Empty, 0, []);
        if (result == 0) return emptyData;

        try
        {
            var decryptedContent = DecryptStringFromBytes_Aes(buffer, model.Key, model.IV);
            return JsonSerializer.Deserialize<SynchronizeData>(decryptedContent) ?? emptyData;
        }
        catch (Exception e)
        {
            throw new PassInvalidException("Can't Synchronize storage :The password is invalid.");
        }
    }

    protected override void ExportStorage(SynchronizeData data, SynchronizationDataModel model)
    {
        using var stream = new StreamWriter(model.FileName);
        var jsonString = JsonSerializer.Serialize(data);
        var encryptedData = EncryptStringToBytes_Aes(jsonString, model.Key, model.IV);
        stream.BaseStream.Write(encryptedData, 0, encryptedData.Length);
        stream.Close();
    }

    private byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = key;
        aesAlg.IV = iv;

        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream msEncrypt = new();
        using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (StreamWriter swEncrypt = new(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }
        return msEncrypt.ToArray();
    }

    private string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key, byte[] iv)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = key;
        aesAlg.IV = iv;

        ICryptoTransform decoder = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream msDecrypt = new(cipherText);
        using CryptoStream csDecrypt = new(msDecrypt, decoder, CryptoStreamMode.Read);
        using StreamReader srDecrypt = new(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}