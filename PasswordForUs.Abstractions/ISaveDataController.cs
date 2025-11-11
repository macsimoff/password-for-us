using PasswordForUs.Abstractions.Models;

namespace PasswordForUs.Abstractions;

public interface ISaveDataController
{
    Task CreateDataAsync(EncryptedData model);
    Task CreateNodesAsync(IEnumerable<EncryptedData> models);
}