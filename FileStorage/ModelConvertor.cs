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
            Name = node.Name,
            Url = node.Url,
            User = node.User,
            ChangeTimeTicks = node.ChangeTimeTicks,
            Data = node.Data
        };
    }

    public static Node Create(NodeData model)
    {
        return new Node()
        {
            Guid = model.Guid,
            Id = model.Id,
            Name = model.Name??string.Empty,
            Url = model.Url??string.Empty,
            User = model.User,
            ChangeTimeTicks = model.ChangeTimeTicks,
            Data = model.Data
        };
    }
}