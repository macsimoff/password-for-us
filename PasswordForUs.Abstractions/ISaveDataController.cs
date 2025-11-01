using PasswordForUs.Abstractions.Models;

namespace PasswordForUs.Abstractions;

public interface ISaveDataController
{
    Task CreateDataAsync(NodeData model);
}