using PasswordForUs.Security;

namespace PasswordForUs.Command.Builder.Factory.Security;

public interface ISecurityFactory
{
    ISecurity Create();
}