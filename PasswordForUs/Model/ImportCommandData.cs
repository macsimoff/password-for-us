namespace PasswordForUs.Model;

public class ImportCommandData(string path)
{
    public string Path { get; } = path;
}