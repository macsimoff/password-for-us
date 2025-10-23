using PasswordForUs.Abstractions.Models;

namespace FileStorage;

public abstract class ModelConvertor
{
    public static NodeData Convert(Node node)
    {
        return new NodeData()
        {
            Guid = node.Guid,
            Id = node.Id,
            Title = node.Name,
            Url = node.Url,
            User = node.User,
            ChangeTimeTicks = node.ChangeTimeTicks,
        };
    }
}