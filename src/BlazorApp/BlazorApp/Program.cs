using BlazorApp.Client.Pages;
using BlazorApp.Components;
using Common.Store;
using Fluxor;
using MediatR;
using MediatR.Examples;
using MediatR.Pipeline;
using Serilog;
using static BlazorApp.Client.Logging.SerilogConfiguration;

namespace BlazorApp;

public static class Program
{
	private static WebApplicationBuilder _builder = null!;
	private static WrappingWriter _mediatorWriter = null!;
	private static IServiceCollection _appServices = null!;
	private static WebApplication _app = null!;

	public static async Task Main(string[] args)
	{
		Log.Logger = CreateLogger();

		try
		{
			Log.Information("Starting web application");

			await StartServer(args).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			Log.Fatal(ex, "Application terminated unexpectedly");
		}
		finally
		{
			await Log.CloseAndFlushAsync().ConfigureAwait(false);
		}
	}
#pragma warning disable MA0051
	private static async Task StartServer(string[] args)
	{
		_builder = WebApplication.CreateBuilder(args);
		_mediatorWriter = new WrappingWriter(Console.Out);
		_appServices = Services(_builder, _mediatorWriter);
		_builder.Logging.ClearProviders();
		_builder.Host.UseSerilog();
		_app = _builder.Build();

		if (!_app.Environment.IsDevelopment())
		{
#pragma warning disable MA0003
			_app.UseExceptionHandler("/Error", true);
#pragma warning restore MA0003
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			_app.UseHsts();
		}
		else
		{
			await MediatRExample(_appServices, _mediatorWriter).ConfigureAwait(false);

			Log.Logger.Debug("## Debug");
			Log.Logger.Information("## Information");
			Log.Logger.Warning("## Warning");
			Log.Logger.Error("## Error");

			_app.UseWebAssemblyDebugging();
		}

		_app.UseHttpsRedirection();

		_app.UseStaticFiles();

		_app.UseAntiforgery();

		_app.MapRazorComponents<App>()
			.AddInteractiveServerRenderMode()
			.AddInteractiveWebAssemblyRenderMode()
			.AddAdditionalAssemblies(typeof(Counter).Assembly);

		await _app.RunAsync().ConfigureAwait(false);

		return;

		IServiceCollection Services(WebApplicationBuilder webApplicationBuilder, WrappingWriter wrappingWriter)
		{
			// Add services to the container.
			var serviceCollection = webApplicationBuilder.Services;

			serviceCollection.AddRazorComponents()
				.AddInteractiveServerComponents()
				.AddInteractiveWebAssemblyComponents();

			serviceCollection.AddSingleton<TextWriter>(wrappingWriter);

			serviceCollection.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(Ping).Assembly, typeof(Sing).Assembly); });

			serviceCollection.AddScoped(typeof(IStreamRequestHandler<Sing, Song>), typeof(SingHandler));
			serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(GenericPipelineBehavior<,>));
			serviceCollection.AddScoped(typeof(IRequestPreProcessor<>), typeof(GenericRequestPreProcessor<>));
			serviceCollection.AddScoped(typeof(IRequestPostProcessor<,>), typeof(GenericRequestPostProcessor<,>));
			serviceCollection.AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(GenericStreamPipelineBehavior<,>));

			serviceCollection.AddFluxor(config =>
			{
				config
					.ScanAssemblies(typeof(CounterRedux).Assembly)
					.UseRouting()
					//.UseReduxDevTools()
					;
			});

			return serviceCollection;
		}

		// ReSharper disable once IdentifierTypo
		async Task MediatRExample(IServiceCollection services, WrappingWriter writer)
		{
			var provider = services.BuildServiceProvider();
			var mediator = provider.GetRequiredService<IMediator>();

#pragma warning disable MA0003
			await Runner.Run(mediator, writer, "ASP.NET Core DI", true).ConfigureAwait(false);
#pragma warning restore MA0003

			await writer.DisposeAsync().ConfigureAwait(false);
		}
	}
}

// var builder = WebApplication.CreateBuilder(args);
//
//
// // Add services to the container.
// builder.Services.AddRazorComponents()
// 	.AddInteractiveServerComponents()
// 	.AddInteractiveWebAssemblyComponents();
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
// 	app.UseWebAssemblyDebugging();
// }
// else
// {
// 	app.UseExceptionHandler("/Error", createScopeForErrors: true);
// 	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
// 	app.UseHsts();
// }
//
// app.UseHttpsRedirection();
//
// app.UseStaticFiles();
// app.UseAntiforgery();
//
// app.MapRazorComponents<App>()
// 	.AddInteractiveServerRenderMode()
// 	.AddInteractiveWebAssemblyRenderMode()
// 	.AddAdditionalAssemblies(typeof(Counter).Assembly);
//
// app.Run();