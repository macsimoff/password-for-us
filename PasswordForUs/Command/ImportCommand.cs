using PasswordForUs.Model;
using PasswordForUs.Security;
using PasswordForUs.Settings;
using PasswordForUsLibrary.DataSynchronizer;
using PasswordForUsLibrary.Import;
using PasswordForUsLibrary.Model;

namespace PasswordForUs.Command;

public class ImportCommand(ImportCommandData commandData, FileImporter fileImporter, Synchronizer synchronizer,ISecurity security)
    : BaseCommandWithSynchronizer(synchronizer, security)
{
    //todo: FileImporter Synchronizer заменить на интерфейсы

    public override void Execute(AppSettings appSettings)
    {
        ImportFile();
        SynchronizeStorage(appSettings);
        Console.WriteLine(Resources.Resources.Import_End);
    }

    private void ImportFile()
    {
        fileImporter.Import(new ImportData() { Path = commandData.Path });
        Console.WriteLine(Resources.Resources.Import_FileParsed);
    }
}