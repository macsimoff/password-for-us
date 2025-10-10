using Microsoft.Extensions.DependencyInjection;
using PasswordForUs.Abstractions;
using PasswordForUsLibrary.PassGenerator;

namespace pw4us.AppConfig;

public abstract class ServiceConfigurator
{
    public static void Configure(ServiceCollection services)
    {
        services.AddTransient<IPassGenerator, PassGenerator>();
    }
}