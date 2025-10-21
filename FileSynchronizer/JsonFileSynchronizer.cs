using System.Text.Json;
using PasswordForUs.Abstractions;
using PasswordForUsLibrary.DataSynchronizer;
using PasswordForUsLibrary.Model;

namespace FileSynchronizer;

public class JsonFileSynchronizer(IRepository repo) : FileSynchronizerBase(repo)
{
    protected override void ExportStorage(SynchronizeData data, SynchronizationDataModel model)
    {
        using var stream = new StreamWriter(model.FileName);
        var jsonString = JsonSerializer.Serialize(data);
        stream.Write(jsonString); // todo: реализовать асинхронную запись
        stream.Close();
    }

    protected override SynchronizeData LoadRemoteStorage(SynchronizationDataModel data)
    {
        using var stream = new StreamReader(data.FileName);
        var fileContent = stream.ReadToEnd();
        var remoteStorage = JsonSerializer.Deserialize<SynchronizeData>(fileContent);
        return remoteStorage ?? throw new ArgumentException($"Problem with file {data.FileName}. Content is not valid.");
    }

}