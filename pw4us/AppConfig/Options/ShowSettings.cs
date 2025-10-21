namespace pw4us.AppConfig.Options;

public class ShowSettings
{
    private bool _id = true;
    private bool _url = true;
    private bool _user;
    private bool _name = true;
    private bool _login = true;
    private bool _password;
    private bool _allData;
    public bool All { get; set; }

    public bool Id
    {
        get => All || _id;
        set => _id = value;
    }

    public bool Url
    {
        get => All || _url;
        set => _url = value;
    }

    public bool User
    {
        get => All || _user;
        set => _user = value;
    }

    public bool Name
    {
        get => All || _name;
        set => _name = value;
    }

    public bool Login
    {
        get => All || _login; 
        set => _login = value;
    }

    public bool Password
    {
        get => All || _password; 
        set => _password = value;
    }

    public bool AllData
    {
        get => All || _allData; 
        set => _allData = value;
    }
    public string[] DataNames { get; set; } = Array.Empty<string>();
}