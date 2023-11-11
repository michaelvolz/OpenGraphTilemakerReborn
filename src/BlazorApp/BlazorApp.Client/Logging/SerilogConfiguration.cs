using System.Globalization;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace BlazorApp.Client.Logging;

public static class SerilogConfiguration
{
	public static LoggerConfiguration BaseLoggerConfiguration()
	{
		return new LoggerConfiguration()
			.WriteTo.Console(
				outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}][{SourceContext}] {Message:lj}{NewLine}{Exception}",
				theme: AnsiConsoleTheme.Code,
				applyThemeToRedirectedOutput: true,
				formatProvider: CultureInfo.InvariantCulture
			)
			.Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"));
	}

	public static IConfigurationRoot ConfigureAppSettings(string pathPrefix = "")
	{
		var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

		Console.WriteLine($"## Environment = {environmentName}");

		return new ConfigurationBuilder()
			.SetBasePath(Path.Combine(pathPrefix, Directory.GetCurrentDirectory()))
#pragma warning disable MA0003
			.AddJsonFile("appsettings.json", true, true)
			.AddJsonFile($"appsettings.{environmentName}.json", true, true)
			.Build();
	}

	public static Logger CreateLogger(string pathPrefix = "")
	{
		var configuration = ConfigureAppSettings(pathPrefix);
		var defaultLogLevel = configuration["Logging:LogLevel:Default"];
		var level = Enum.Parse<LogEventLevel>(defaultLogLevel ?? "Information");

		if (level is LogEventLevel.Verbose or LogEventLevel.Debug or LogEventLevel.Information)
			Console.WriteLine($"## defaultLogLevel: '{pathPrefix} {defaultLogLevel}'");

		var loggerConfiguration = BaseLoggerConfiguration();

		switch (level)
		{
			case LogEventLevel.Verbose:
				loggerConfiguration.MinimumLevel.Verbose();
				break;
			case LogEventLevel.Debug:
				loggerConfiguration.MinimumLevel.Debug();
				break;
			case LogEventLevel.Information:
				loggerConfiguration.MinimumLevel.Information();
				break;
			case LogEventLevel.Warning:
				loggerConfiguration.MinimumLevel.Warning();
				break;
			case LogEventLevel.Error:
				loggerConfiguration.MinimumLevel.Error();
				break;
			case LogEventLevel.Fatal:
				loggerConfiguration.MinimumLevel.Fatal();
				break;
			default:
#pragma warning disable MA0015, MA0003, CA2208
				throw new ArgumentOutOfRangeException(nameof(level), level, null);
#pragma warning restore CA2208, MA0003, MA0015
		}

		return loggerConfiguration.CreateLogger();
	}
}