using PasswordForUs.Settings;

namespace PasswordForUs.Command;

public class SetPassCommand(string pass) : ICommand
{
    public void Execute(AppSettings appSettings)
    {
        appSettings.Pass = string.IsNullOrEmpty(pass) ? GetPass().Trim() : pass.Trim();

        if(appSettings.PassHashIteration <= 0)
            appSettings.PassHashIteration = GetIteration();
    }
    
    private static string GetPass()
    {
        while (true)
        {
            Console.WriteLine(Resources.Resources.SetPass_WritePromt);

            var pass = Console.ReadLine();
            if (!string.IsNullOrEmpty(pass))
                return pass;
            
            Console.WriteLine(Resources.Resources.SetPass_IsEmpty);
        }
    }
    
    private static int GetIteration()
    {
        while (true)
        {
            Console.WriteLine(Resources.Resources.SetPass_HashIterationPromt);
            var input = Console.ReadLine();

            if (int.TryParse(input, out int iteration) && iteration > 0)
            {
                return iteration;
            }

            Console.WriteLine(Resources.Resources.SetPass_InvalidInputInteger);
        }
    }
}   