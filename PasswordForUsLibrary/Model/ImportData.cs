namespace PasswordForUsLibrary.Model;

public class ImportData
{
    private bool GetPathIsValid(string s)
    {
        if (string.IsNullOrEmpty(s) || s.IndexOfAny(System.IO.Path.GetInvalidPathChars()) != -1) return false;
        
        try
        {
            var info = new FileInfo(s);
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    private readonly string _path = string.Empty;

    public string Path
    {
        get => _path;
        init
        {
            PathIsValid = GetPathIsValid(value);
            _path = value;
        }
    }

    public bool PathIsValid { get; private set; } = false;
}