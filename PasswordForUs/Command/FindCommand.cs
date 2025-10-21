using PasswordForUs.Abstractions.Models;
using PasswordForUs.Model;
using PasswordForUs.Settings;
using PasswordForUsLibrary.DataController;

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
        Console.WriteLine(Resources.Resources.FindCommand_TryingToFind, 
            _commandData.Id, _commandData.UrlText, _commandData.NameText);
        NodeData[] items;

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
                              + Resources.Resources.OpenFilePrompt);
            return;            
        }

        if (items.Length == 0)
        {
            Console.WriteLine(Resources.Resources.FindCommand_NotFound);
        }
        else
        {
            Console.WriteLine(Resources.Resources.FindCommand_Done);
            _nodeWriter.Write(items, appSettings.ShowSettings);
        }

    }
}