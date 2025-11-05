using System.Text;
using Spectre.Console.Cli;

namespace pw4us.Infrastructure.Settings;

public class PassCommandSettings: LogCommandSettings
{
    //todo: LocalizedDescription
    [CommandOption("-p|--password <PASSWORD>")]
    public string Pass { get; set; } = string.Empty;

    public byte[] PassBytes => Encoding.UTF8.GetBytes(Pass);
}