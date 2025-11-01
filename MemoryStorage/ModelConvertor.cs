namespace MemoryStorage;

internal static class ModelConvertor
{
    public static NodeData Convert(PasswordForUs.Abstractions.Models.NodeData node)
    {
        return new NodeData
        (
            node.Guid,
            node.ChangeTimeTicks,
            node.Id,
            node.User,
            node.Url,
            node.Name,
            node.Login,
            node.Password,
            new Dictionary<string, string>(node.Data)
        );
    }

    public static PasswordForUs.Abstractions.Models.NodeData Convert(NodeData node)
    {
        return new PasswordForUs.Abstractions.Models.NodeData(
            node.Guid,
            node.ChangeTimeTicks,
            node.Id,
            node.User,
            node.Url,
            node.Title,
            node.Login,
            node.Password,
            node.Data);
    }
}