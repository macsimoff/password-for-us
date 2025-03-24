using PasswordForUsLibrary.Model;

namespace PasswordForUsLibrary.Import.StringParser;

public interface IStringParser
{
    NodeDataModel CreateNodeData(string nodeDataString);
}