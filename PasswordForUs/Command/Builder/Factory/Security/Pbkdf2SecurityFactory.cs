using PasswordForUs.Security;

namespace PasswordForUs.Command.Builder.Factory.Security;

public class Pbkdf2SecurityFactory: ISecurityFactory
{
    public ISecurity Create()
    {
        return new Pbkdf2Security();
    }
}