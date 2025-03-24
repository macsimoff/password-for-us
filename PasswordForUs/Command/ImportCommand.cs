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
        Console.WriteLine("End import command.");
    }

    private void ImportFile()
    {
        fileImporter.Import(new ImportData() { Path = commandData.Path });
        Console.WriteLine("File has been parsed.");
    }
}