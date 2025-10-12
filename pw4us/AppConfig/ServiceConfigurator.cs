using Microsoft.Extensions.DependencyInjection;
using PasswordForUs.Abstractions;
using PasswordForUsLibrary.PassGenerator;
using pw4us.Infrastructure;
using Serilog;

namespace pw4us.AppConfig;

public abstract class ServiceConfigurator
{
    public static void Configure(ServiceCollection services)
    {
        services.AddLogging(configure =>
            configure.AddSerilog(new LoggerConfiguration()
                .MinimumLevel.ControlledBy(LogInterceptor.LogLevel)
                .Enrich.With<LoggingEnricher>()
                .WriteTo.Map(LoggingEnricher.LogFilePathPropertyName,
                    (logFilePath, wt) => wt.File(
                        path: $"{logFilePath}",
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 14
                    ), 1)
                .CreateLogger()
            )
        );
        services.AddTransient<IPassGenerator, PassGenerator>();
    }
}