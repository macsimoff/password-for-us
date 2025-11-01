using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PasswordForUs.Abstractions;
using PasswordForUs.Abstractions.Models;
using pw4us.AppConfig.Options;
using pw4us.Attributes;
using pw4us.Infrastructure;
using pw4us.Rendering;
using pw4us.Resources;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Extensions;

namespace pw4us.Commands;

public class CreateCommand(
    ILogger<CreateCommand> logger,
    IOptions<ShowSettings> showSettings,
    IOptions<UserSettings> userSettings,
    IOptions<GeneratePassSettings> generatePassSettings,
    ISaveDataController controller,
    IPassGenerator generator): AsyncCommand<CreateCommand.Settings>
{
    public class Settings :LogCommandSettings
    {
        //todo: Description
        [CommandArgument(0,"[URL]")]
        [LocalizedDescription(nameof(DescriptionResources.FC_Id))]
        public string? Url { get; set; }

        //todo: Description
        [CommandOption("-n|--name <NAME>")]
        [LocalizedDescription(nameof(DescriptionResources.FC_Id))]
        public string? Name { get; set; }

        //todo: Description
        [CommandOption("-l|--login <LOGIN>")]
        [LocalizedDescription(nameof(DescriptionResources.FC_Id))]
        public string? Login { get; set; }

        //todo: Description
        [CommandOption("-p|--pas <PASSWORD>")]
        [LocalizedDescription(nameof(DescriptionResources.FC_Id))]
        public string? Password { get; set; }

        //todo: Description
        [CommandOption("-k|--dataKey <KEY>")]
        [LocalizedDescription(nameof(DescriptionResources.FC_Id))]
        public string[]? DataNames { get; set; }
        
        //todo: Description
        [CommandOption("-v|--dataValue <VALUES>")]
        [LocalizedDescription(nameof(DescriptionResources.FC_Id))]
        public string[]? DataValues { get; set; }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Url)
                   &&string.IsNullOrEmpty(Name) 
                   && string.IsNullOrEmpty(Login) 
                   && string.IsNullOrEmpty(Password)
                   && (DataNames == null ||  DataNames.Length == 0) 
                   && (DataValues == null || DataValues.Length == 0);
        }
    }

    private const int PassDefValue = 12;
    private static readonly string EditeEmoji = AnsiConsoleHelpers.GetEditeEmoji();

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        LogSettings(settings);

        var node = await CreateNode(settings);

        var icon = AnsiConsoleHelpers.GetFloppyEmoji();
        AnsiConsole.Markup($"[yellow]{icon} Save new entry :[/] ");
        var spinnerAnimation = AnsiConsoleHelpers.GetSpinnerAnimation();
        
        await controller
            .CreateDataAsync(node)
            .Spinner(
                spinnerAnimation,
                new Style(foreground: Color.Blue));
        
        AnsiConsole.Markup("[green]done[/]");
        
        NodeRenderer.Render("[blue]CREATE RESULT[/]",showSettings.Value, node);
        
        return 0;
    }

    private void LogSettings(Settings settings)
    {
        logger.LogDebug("Settings created -> Url : {Url}; Name : {Name}; Login {Login};"+
                        "Password : {Password}; Data : {DataNames}; DataValues : {DataValues}", 
            settings.Url, 
            settings.Name, 
            settings.Login, 
            settings.Password,
            settings.DataNames,
            settings.DataValues);
    }

    private async Task<NodeData> CreateNode(Settings settings)
    {
        return new NodeData()
        {
            Id = -1,
            ChangeTimeTicks = DateTime.UtcNow.Ticks,
            Guid = Guid.NewGuid(),
            Name = await GetName(settings),
            Url = GetUrl(settings),
            Login = await GetLogin(settings),
            Password = GetPassword(settings),
            User = GetUser(),
            Data = await GetData(settings)
        };
    }


    private async Task<string?> GetName(Settings settings)
    {
        //To search we need a name or a URL, if there is neither, then there must be a name
        //  If either the name or URL exists, then we don't need anything else.
        if (!string.IsNullOrEmpty(settings.Name) || !string.IsNullOrEmpty(settings.Url)) return settings.Name;
        
        var name =await AnsiConsole.PromptAsync(new TextPrompt<string?>($"{EditeEmoji} Enter a name of entry : ").AllowEmpty());
        return name;

    }

    private string? GetUrl(Settings settings)
    {
        return settings.IsEmpty() ? 
            AnsiConsole.Prompt(new TextPrompt<string?>($"{EditeEmoji} Enter a Url : ").AllowEmpty()) 
            : settings.Url;
    }

    private async Task<string?> GetLogin(Settings settings)
    {
        if (!string.IsNullOrEmpty(settings.Login) || !string.IsNullOrEmpty(userSettings.Value.Login))
            return !string.IsNullOrEmpty(settings.Login)
                ? settings.Login
                : userSettings.Value.Login;
        
        var login = await AnsiConsole.PromptAsync(new TextPrompt<string?>($"{EditeEmoji} Enter a Login : ").AllowEmpty());
        return login;

    }

    private string? GetPassword(Settings settings)
    {
        if (!string.IsNullOrEmpty(settings.Password)) return settings.Password;

        var needPass = true;
        if(string.IsNullOrEmpty(settings.Login))
            needPass = AnsiConsole.Prompt(
                new TextPrompt<bool>($"{EditeEmoji} generate a password for you : ")
                    .AddChoice(true)
                    .AddChoice(false)
                    .DefaultValue(true)
                    .WithConverter(choice => choice ? "y" : "n"));

        if (!needPass) return null;
        
        var passLength = generatePassSettings.Value.Length > 0 ? generatePassSettings.Value.Length : PassDefValue;
        var alphabet = generatePassSettings.Value.Alphabet.Length > 0 ? generatePassSettings.Value.Alphabet : null;
        var pass = generator.Generate(passLength, alphabet);
        return pass;

    }

    private string? GetUser()
    {
        return !string.IsNullOrEmpty(userSettings.Value.Username) ? userSettings.Value.Username : null;
    }

    private async Task<Dictionary<string, string>> GetData(Settings settings)
    {
        var names = settings.DataNames?? [];
        var values = settings.DataValues?? [];
        var data = new Dictionary<string, string>();
        
        // Pair existing names with values or prompt for missing values
        for (var i = 0; i < names.Length; i++)
        {
            if (i < values.Length)
            {
                data.Add(names[i], values[i]);
            }
            else
            {
                var val = await AnsiConsole.PromptAsync(
                    new TextPrompt<string>($"{EditeEmoji} Enter a [yellow]value[/] for '{names[i]}' data : "));
                data.Add(names[i], val);
            }
        }

        // Prompt for names for remaining values
        for (var j = names.Length; j < values.Length; j++)
        {
            var name = await AnsiConsole.PromptAsync(
                new TextPrompt<string>($"{EditeEmoji} Enter a [yellow]name[/] for '{values[j]}' data : "));
            data.Add(name, values[j]);
        }

        return data;
    }
}
