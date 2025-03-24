using FileSynchronizer;
using MemoryStorage;
using PasswordForUs.Command.Builder.Factory.Repo;
using PasswordForUs.Command.Builder.Factory.Security;
using PasswordForUs.Command.Builder.Factory.Sync;
using PasswordForUs.Model;
using PasswordForUsLibrary.Import;
using PasswordForUsLibrary.Import.FileParser;
using PasswordForUsLibrary.Import.FileReader;
using PasswordForUsLibrary.Import.StringParser;

namespace PasswordForUs.Command.Builder;

public class ImportCommandBuilder(IRepositoryFactory repoFactory, ISynchronizerFactory syncFactory,
    ISecurityFactory securityFactory): ICommandBuilder
{
    public ICommand Build(string[] commandData)
    {
        var data = CreateImportCommandData(commandData);
        return BuildImportCommand(data);
    }

    private ImportCommandData CreateImportCommandData(string[] commandData)
    {
        return commandData.Length > 0 ? new ImportCommandData(commandData[0]) : new ImportCommandData(string.Empty);
    }

    private ICommand BuildImportCommand(ImportCommandData data)
    {
        var repo = repoFactory.Create();
        var synchronizer = syncFactory.Create(repo);
        
        var fileInfo = new FileInfo(data.Path);
        var fileImporter = new FileImporter(FileParser(fileInfo.Extension), repo);

        var security = securityFactory.Create();
        return new ImportCommand(data, fileImporter, synchronizer,security);
    }

    private IFileParser FileParser(string extension)
    {
        return extension switch
        {
            ".txt" => new HomeFileParser(new ReaderByBlankLinesDelimiter(), new HomeFileStringParser()),
            ".json" => new JsonFileParser(),
            _ => new HomeFileParser(new ReaderByBlankLinesDelimiter(), new HomeFileStringParser())
        };
    }
}