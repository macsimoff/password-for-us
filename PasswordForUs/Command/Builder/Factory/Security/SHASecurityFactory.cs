using PasswordForUs.Security;

namespace PasswordForUs.Command.Builder.Factory.Security;

public class ShaSecurityFactory : ISecurityFactory
{
    public ISecurity Create()
    {
        return new ShaSecurity();
    }
}