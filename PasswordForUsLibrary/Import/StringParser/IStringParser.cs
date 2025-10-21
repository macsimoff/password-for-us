using PasswordForUs.Abstractions.Models;

namespace PasswordForUsLibrary.Import.StringParser;

public interface IStringParser
{
    NodeData CreateNodeData(string nodeDataString);
}