using pw4us.Commands;

namespace pw4us.AppConfig;

public class GeneratePassSettings
{
    public int Length { get; set; }
    public string[] Alphabet { get; set; } = [];
}