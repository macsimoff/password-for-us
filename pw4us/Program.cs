using System.Globalization;
using Spectre.Console.Cli;
using Microsoft.Extensions.DependencyInjection;
using pw4us.AppConfig;
using pw4us.Infrastructure;
using Microsoft.Extensions.Configuration;

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
    var interceptor = new LogInterceptor(configuration);
    cfg.SetInterceptor(interceptor);
});

app.Run(args);