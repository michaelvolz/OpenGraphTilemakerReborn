using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.SystemConsole.Themes;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var levelSwitch = new LoggingLevelSwitch();
Log.Logger = new LoggerConfiguration()
	.WriteTo.Console(
		outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
		theme: AnsiConsoleTheme.Code,
		applyThemeToRedirectedOutput: true
	)
	.MinimumLevel.ControlledBy(levelSwitch)
	.Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"))
	.CreateLogger();

builder.Services.RemoveAll<ILogger>();

builder.Logging.AddSerilog();

builder.Services.AddFluxor(config => {
	config
		.ScanAssemblies(typeof(Program).Assembly)
		.ScanAssemblies(typeof(Common.Store.CounterRedux).Assembly)
		.UseRouting()
		//.UseReduxDevTools()
		;
});

Log.Information("### Starting App");

await builder.Build().RunAsync();



// var builder = WebAssemblyHostBuilder.CreateDefault(args);
//
// await builder.Build().RunAsync();
