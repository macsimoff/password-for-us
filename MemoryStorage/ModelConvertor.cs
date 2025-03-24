using PasswordForUsLibrary.Model;

namespace MemoryStorage;

internal static class ModelConvertor
{
    public static NodeData Convert(NodeDataModel node)
    {
        return new NodeData
        (
            node.Guid,
            node.ChangeTimeTicks,
            node.Id,
            node.User,
            node.Url,
            node.Title,
            node.Login,
            node.Password,
            new Dictionary<string, string>(node.Data)
        );
    }

    public static NodeDataModel Convert(NodeData node)
    {
        return new NodeDataModel(
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