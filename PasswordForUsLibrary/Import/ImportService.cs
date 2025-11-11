using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;
using PasswordForUsLibrary.Import.FileParser;
using PasswordForUsLibrary.Import.FileReader;
using PasswordForUsLibrary.Import.StringParser;

namespace PasswordForUsLibrary.Import;

public class ImportService : IImportService
{
    public async Task<List<NodeData>> ImportAsync(string path, string? format, byte[] passBytes, 
        ISaveDataController saveController, IEncryptionService encryption, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Path is empty", nameof(path));
        if (!File.Exists(path)) throw new FileNotFoundException("File not found", path);

        var parser = GetParser(format, path);
        var nodes = new List<NodeData>();
        var importList = new List<EncryptedData>();
        using var reader = new StreamReader(path);
        await foreach (var node in parser.ParseNodesAsync(reader).WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (node.Guid == Guid.Empty) node.Guid = Guid.NewGuid();
            if (node.ChangeTimeTicks == 0) node.ChangeTimeTicks = DateTime.UtcNow.Ticks;
            node.Id = -1;
            
            importList.Add(encryption.Encrypt(passBytes, node));
            
            nodes.Add(node);
        }
        
        await saveController.CreateNodesAsync(importList);

        return nodes;
    }

    private static IFileParser GetParser(string? format, string path)
    {
        var ext = Path.GetExtension(path).ToLowerInvariant();
        var fmt = string.IsNullOrWhiteSpace(format) ? ext : format.Trim().ToLowerInvariant();
        fmt = fmt switch
        {
            ".txt" => "home",
            "txt" => "home",
            ".json" => "json",
            "json" => "json",
            ".csv" => "csv",
            "csv" => "csv",
            _ => fmt
        };
        return fmt switch
        {
            "json" => new JsonFileParser(),
            "csv" => new CsvFileParser(new LineFileReader(), new GoogleCsvStringParser()),
            _ => new HomeFileParser(new LineFileReader(), new HomeFileStringParser())
        };
    }
}

