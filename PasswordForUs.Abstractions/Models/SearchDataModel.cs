namespace PasswordForUs.Abstractions.Models;

public class SearchDataModel
{
    public SearchDataModel(string text) : this(null,text, text)
    {
    }

    public SearchDataModel(int id): this(id,null,null)
    {
    }

    public SearchDataModel(string name, string url): this(null,name,url)
    {
    }
    public SearchDataModel(int? id, string? name, string? url)
    {
        if (id != null)
        {
            ById = true;
            Id = id.Value;
        }

        if (!string.IsNullOrEmpty(name))
        {
            ByName = true;
            Name = name;
        }
        else
        {
            Name = string.Empty;
        }

        if (!string.IsNullOrEmpty(url))
        {
            ByUrl = true;
            Url = url;
        }
        else
        {
            Url = string.Empty;
        }
    }

    public bool ById { get; }
    public int Id { get; }


    public bool ByName { get; }
    public string Name { get; }

    public bool ByUrl { get; }
    public string Url { get; }
}