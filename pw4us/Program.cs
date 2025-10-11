using System.Globalization;
using Spectre.Console.Cli;
using Microsoft.Extensions.DependencyInjection;
using pw4us.AppConfig;
using pw4us.Infrastructure;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var services = new ServiceCollection();
ServiceConfigurator.Configure(services);

var app = new CommandApp(new MSDITypeRegistrar(services));
app.Configure(CommandConfigurator.Configure);

app.Run(args);