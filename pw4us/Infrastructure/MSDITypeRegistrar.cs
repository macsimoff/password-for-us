using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace pw4us.Infrastructure;

public class MSDITypeRegistrar(ServiceCollection services) :  ITypeRegistrar
{
    public ITypeResolver Build()
    {
        return new MSDITypeResolver(services.BuildServiceProvider());
    }

    public void Register(Type service, Type implementation)
    {
        services.AddSingleton(service, implementation);
    }

    public void RegisterInstance(Type service, object implementation)
    {
        services.AddSingleton(service, implementation);
    }

    public void RegisterLazy(Type service, Func<object> func)
    {
        if (func is null)
        {
            throw new ArgumentNullException(nameof(func));
        }

        services.AddSingleton(service, (provider) => func());
    }
}