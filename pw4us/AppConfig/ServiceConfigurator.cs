using FileStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PasswordForUs.Abstractions;
using PasswordForUsLibrary.DataController;
using PasswordForUsLibrary.PassGenerator;
using pw4us.AppConfig.Options;
using pw4us.Infrastructure;
using Serilog;

namespace pw4us.AppConfig;

public abstract class ServiceConfigurator
{
    public static void Configure(ServiceCollection services, IConfiguration configuration)
    {
        InitializeLoggingDirectory();

        services.AddSingleton(configuration);

        services.AddOptions<GeneratePassSettings>()
            .BindConfiguration("GeneratePass")
            .ValidateOnStart();
        services.AddOptions<ShowSettings>()
            .BindConfiguration("Show")
            .ValidateOnStart();
        services.AddOptions<UserSettings>()
            .BindConfiguration("User")
            .ValidateOnStart();

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
        
        services.AddSingleton<IRepository,Storage>(n => new Storage("data.json"));
        services.AddTransient<IPassGenerator, PassGenerator>();
        services.AddTransient<ISearchDataController,SearchDataController>();
        services.AddTransient<ISaveDataController,SaveDataController>();
    }

    private static void InitializeLoggingDirectory()
    {
        var baseDir = AppContext.BaseDirectory;
        var logsDir = Path.Combine(baseDir, "log");
        Directory.CreateDirectory(logsDir);
        LoggingEnricher.Path = Path.Combine(logsDir, "application.log");
    }
}