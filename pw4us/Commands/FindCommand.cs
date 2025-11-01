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

public class FindCommand(
    ILogger<FindCommand> logger,
    IOptions<ShowSettings> options,
    ISearchDataController controller) : AsyncCommand<FindCommand.Settings>
{

    public class Settings : LogCommandSettings
    {
        [CommandOption("-i|--id <ID>")]
        [LocalizedDescription(nameof(DescriptionResources.FC_Id))]
        public int? Id { get; set; }

        [CommandOption("-u|--url <URL>")]
        [LocalizedDescription(nameof(DescriptionResources.FC_Url))]
        public string? Url { get; set; }
        
        //todo: Description
        [CommandOption("-n|--name <name>")]
        [LocalizedDescription(nameof(DescriptionResources.FC_Url))]
        public string? Name { get; set; }

        [CommandArgument(0,"[text]")]
        [LocalizedDescription(nameof(DescriptionResources.FC_Name))]
        public string? Text { get; set; }

        public override ValidationResult Validate()
        {
            if(!Id.HasValue && Text == null && Url == null)
                return ValidationResult.Error("Please specify a search parameter.");
            return base.Validate();
        }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        LogCommandDebugInfo(settings);

        var spinnerAnimation = AnsiConsoleHelpers.GetSpinnerAnimation();

        var name = string.IsNullOrEmpty(settings.Text)? settings.Name : settings.Text;
        var url = string.IsNullOrEmpty(settings.Text) ? settings.Url : settings.Text;
        var data = await controller
            .SearchAsync(new SearchDataModel(settings.Id, name, url))
            .Spinner(
                spinnerAnimation,
                new Style(foreground: Color.Blue));

        DrawTable(data);

        return 0;
    }

    private void LogCommandDebugInfo(Settings settings)
    {
        logger.LogDebug("Executing FindCommand with Settings -> Id: {Id}, Url: {Url}, Name: {Name}",
            settings.Id, settings.Url, settings.Text);
        logger.LogDebug("Executing FindCommand with ShowSettings -> " +
                        "All: {All}, Id: {Id}, Name: {Name}, Url: {Url}, Login: {Login}," +
                        "User: {User}, Password: {Password}, AllData: {AllData}, Data: {DataNames}",
            options.Value.All,
            options.Value.Id,
            options.Value.Name,
            options.Value.Url,
            options.Value.Login,
            options.Value.User,
            options.Value.Password,
            options.Value.AllData,
            options.Value.DataNames.ToString());
    }

    private void DrawTable(IEnumerable<NodeData> data)
    {
        var rows = data?.ToList() ?? new List<NodeData>();
        if (rows.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No results found.[/]");
            return;
        }

        NodeRenderer.Render("[blue]SEARCH RESULT[/]",options.Value, data);
    }
}