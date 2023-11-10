using Common.Store;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;
using static BlazorApp.Client.Logging.SerilogConfiguration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Log.Logger = CreateLogger();

// SerilogConfiguration.BaseLoggerConfiguration()
// 	.MinimumLevel.Information()
// 	.CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services.AddFluxor(fluxorOptions =>
{
	fluxorOptions
		.ScanAssemblies(typeof(Program).Assembly)
		.ScanAssemblies(typeof(CounterRedux).Assembly)
		.UseRouting()
		//.UseReduxDevTools()
		;
});

Log.Debug("### Starting WebAssembly App");
Log.Information("### Starting WebAssembly App");

await builder.Build().RunAsync().ConfigureAwait(false);