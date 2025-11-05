using System.Globalization;
using System.Security.Cryptography;
using Spectre.Console.Cli;
using Microsoft.Extensions.DependencyInjection;
using pw4us.AppConfig;
using pw4us.Infrastructure;
using Microsoft.Extensions.Configuration;
using pw4us.Infrastructure.Interceptor;
using Spectre.Console;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;


var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("app_settings.json", optional: true, reloadOnChange: false)
                #if DEBUG
                    .AddJsonFile("app_settings.development.json", optional: true, reloadOnChange: false)
                #endif
                    .AddEnvironmentVariables("PW4US_")
                    .Build();

var services = new ServiceCollection();
ServiceConfigurator.Configure(services, configuration);

var app = new CommandApp(new MSDITypeRegistrar(services));
app.Configure(cfg =>
{
    CommandConfigurator.Configure(cfg);
    var logInterceptor = new LogInterceptor(configuration);
    var passInterceptor = new PasswordInterceptor();
    cfg.SetInterceptor(logInterceptor);
    cfg.SetInterceptor(passInterceptor);
});

try
{
    app.Run(args);
}
catch (AuthenticationTagMismatchException e)
{
    AnsiConsole.MarkupLine($"[red]Wrong password...[/]");
}
catch (Exception exe)
{
    AnsiConsole.WriteException(exe, ExceptionFormats.ShortenEverything);
}
