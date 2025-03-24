namespace PasswordForUs.ConsoleExtension;

public static class AppConsoleExtension
{
    public static void WriteRowSeparator(int rowLength)
    {
        for (var i = 0; i < rowLength; i++)
        {
            Console.Write('-');
        }
    }

    public static void WriteGap(int length)
    {
        for (var i = 0; i < length; i++)
        {
            Console.Write(' ');
        }
    }
}