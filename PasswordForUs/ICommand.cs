using PasswordForUs.Settings;

namespace PasswordForUs;

public interface ICommand
{
    void Execute(AppSettings appSettings);
}