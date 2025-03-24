namespace PasswordForUs;

public class TabCompletionHandler
{
    private readonly List<string> _commands;
    private int _currentIndex;
    private string _currentInput;

    public TabCompletionHandler(IEnumerable<string> commands)
    {
        _commands = commands.ToList();
        _currentIndex = -1;
        _currentInput = string.Empty;
    }

    public string HandleTabCompletion(string input)
    {
        if (_currentInput != input)
        {
            _currentInput = input;
            _currentIndex = -1;
        }

        var matches = _commands.Where(c => c.StartsWith(input, StringComparison.OrdinalIgnoreCase)).ToList();

        if (matches.Count == 0)
        {
            return input;
        }

        _currentIndex = (_currentIndex + 1) % matches.Count;
        return matches[_currentIndex];
    }
}