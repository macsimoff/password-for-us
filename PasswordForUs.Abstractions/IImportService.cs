using PasswordForUs.Abstractions.Models;

namespace PasswordForUs.Abstractions;

public interface IImportService
{
    Task<List<NodeData>> ImportAsync(
        string path,
        string? format,
        byte[] passBytes,
        ISaveDataController saveController,
        IEncryptionService encryption,
        CancellationToken cancellationToken = default);
}

