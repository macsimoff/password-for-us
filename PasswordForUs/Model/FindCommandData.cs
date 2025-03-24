namespace PasswordForUs.Model;

public class FindCommandData
{
    public int? Id { get; set; }
    public string UrlText { get; set; }
    public string NameText { get; set; }

    public FindCommandData(string text)
    {
        UrlText = text;
        NameText = text;
        Id = null;
    }

    public FindCommandData(): this(string.Empty)
    {
    }
}