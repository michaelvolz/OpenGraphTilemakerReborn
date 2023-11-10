using Common.Serilog;
using Common.Store;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

SerilogConfiguration.LoggerConfiguration()
	.MinimumLevel.Information()
	.CreateLogger();
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