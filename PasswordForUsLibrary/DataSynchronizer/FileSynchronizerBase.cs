using PasswordForUsLibrary.DataRepository;
using PasswordForUsLibrary.Model;

namespace PasswordForUsLibrary.DataSynchronizer;

public abstract class FileSynchronizerBase(IRepository repo) : Synchronizer(repo)
{
    public override void SynchronizeStorage(SynchronizationDataModel data)
    {
        ValidateDataModel(data);

        var remoteStorage = LoadRemoteStorage(data);
        
        var storageVersion = Repo.GetStorageDataVersion();
        
        if (storageVersion == remoteStorage.Version) return;
        
        var storageData = Repo.GetAll().ToList();
        var storageChangeTime = Repo.GetStorageDataChangeTime();
        var localStorage = new SynchronizeData(storageVersion, storageChangeTime, storageData);
        var mergeStorage = MergeData(localStorage, remoteStorage);
            
        if (mergeStorage.ChangeTimeTicks >= remoteStorage.ChangeTimeTicks 
            && mergeStorage.Version != remoteStorage.Version)
            ExportStorage(mergeStorage, data);
        if (mergeStorage.ChangeTimeTicks >= localStorage.ChangeTimeTicks
            && mergeStorage.Version != localStorage.Version)
            ImportStorage(mergeStorage);
    }

    protected virtual void ValidateDataModel(SynchronizationDataModel data)
    {
        if(!File.Exists(data.FileName))
            throw new ArgumentException($"File {data.FileName} does not exist.");
    }
    protected virtual void ImportStorage(SynchronizeData synchronizeData)
    {
        foreach (var node in synchronizeData.Data)
        {
            Repo.ImportNode(node);
        }

        Repo.SetVersion(synchronizeData.Version, synchronizeData.ChangeTimeTicks);
    }
    protected abstract SynchronizeData LoadRemoteStorage(SynchronizationDataModel data);
    protected abstract void ExportStorage(SynchronizeData data, SynchronizationDataModel model);
    
}