namespace PasswordForUs.Model;

public class OpenCommandData
{
    public OpenCommandData(string path)
    {
        Path = path;
    }

    public string Path { get; }
}