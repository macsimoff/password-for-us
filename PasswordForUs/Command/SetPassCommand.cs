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
            Console.WriteLine("Write password please:");

            var pass = Console.ReadLine();
            if (!string.IsNullOrEmpty(pass))
                return pass;
            
            Console.WriteLine("Password is empty.");
        }
    }
    
    private static int GetIteration()
    {
        while (true)
        {
            Console.WriteLine("Write hash iteration for password please:");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int iteration) && iteration > 0)
            {
                return iteration;
            }

            Console.WriteLine("Invalid input. Please enter a positive integer.");
        }
    }
}   