using PasswordForUs.Abstractions.Models;

namespace PasswordForUs.Abstractions;

public interface ISaveDataController
{
    Task AddNewDataAsync(NodeData model);
}