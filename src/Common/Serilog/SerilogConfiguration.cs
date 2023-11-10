using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Common.Serilog;

public static class SerilogConfiguration
{
	public static LoggerConfiguration LoggerConfiguration()
	{
		return new LoggerConfiguration()
			.WriteTo.Console(
				outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}][{SourceContext}] {Message:lj}{NewLine}{Exception}",
				theme: AnsiConsoleTheme.Code,
				applyThemeToRedirectedOutput: true
			)
			.Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"));
	}
}