using PasswordForUs.ConsoleExtension;
using PasswordForUs.Model;
using PasswordForUs.Settings;
using PasswordForUsLibrary.DataController;
using PasswordForUsLibrary.Model;

namespace PasswordForUs.Command;

public class FindCommand: ICommand
{
    private readonly FindCommandData _commandData;
    private readonly SearchDataController _controller;
    private readonly NodeWriter _nodeWriter;

    public FindCommand(FindCommandData commandData, SearchDataController controller, NodeWriter nodeWriter)
    {
        _commandData = commandData;
        _controller = controller;
        _nodeWriter = nodeWriter;
    }

    public void Execute(AppSettings appSettings)
    {
        Console.WriteLine($"Trying to find {_commandData.UrlText}...");
        NodeDataModel[] items;

        try
        {
            items = _controller.Search(new SearchDataModel(
                _commandData.Id,
                _commandData.UrlText,
                _commandData.NameText)
            ).ToArray();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message 
                              + " You need to open(o) or to import(i) the password file or to add new node to the storage.");
            return;            
        }

        if (items.Length == 0)
        {
            Console.WriteLine($"The program didn't find anything.");
        }
        else
        {
            Console.WriteLine("done");
            _nodeWriter.Write(items, appSettings.ShowSettings);
        }

    }
}