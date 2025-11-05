using PasswordForUs.Abstractions.Models;

namespace FileStorage;

public abstract class ModelConvertor
{
    public static EncryptedData Convert(Node node)
    {
        var bytes = string.IsNullOrEmpty(node.SecretData)
            ? []
            : System.Convert.FromBase64String(node.SecretData);

        return new EncryptedData()
        {
            Guid = node.Guid,
            Id = node.Id,
            Name = node.Name,
            Url = node.Url,
            User = node.User,
            ChangeTimeTicks = node.ChangeTimeTicks,
            Bytes = bytes
        };
    }

    public static Node Create(EncryptedData model)
    {
        var secret = (model.Bytes.Length > 0)
            ? System.Convert.ToBase64String(model.Bytes)
            : string.Empty;

        return new Node()
        {
            Guid = model.Guid,
            Id = model.Id,
            Name = model.Name ?? string.Empty,
            Url = model.Url ?? string.Empty,
            User = model.User,
            ChangeTimeTicks = model.ChangeTimeTicks,
            SecretData = secret
        };
    }
}