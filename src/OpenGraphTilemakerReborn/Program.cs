using Fluxor;
using MediatR;
using MediatR.Examples;
using MediatR.Pipeline;
using OpenGraphTilemakerReborn.Components;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace OpenGraphTilemakerReborn
{
    public class Program
    {
        private static WebApplicationBuilder _builder = null!;
        private static WrappingWriter _mediatRWriter = null!;
        private static IServiceCollection _appServices = null!;
        private static WebApplication _app = null!;

        public static async Task Main(string[] args)
        {
            Log.Logger = CreateLogger();

            try
            {
                Log.Information("Starting web application");

                await StartServer(args);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                await Log.CloseAndFlushAsync();
            }
        }

        private static Logger CreateLogger()
        {
            var config = new LoggerConfiguration()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                    theme: AnsiConsoleTheme.Code,
                    applyThemeToRedirectedOutput: true
                );

            static IConfigurationRoot GetIConfigurationRoot()
            {
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                Console.WriteLine($"## Environment = {environmentName}");

                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                    .Build();

                return config;
            }

            var configuration = GetIConfigurationRoot();
            var defaultLogLevel = configuration["Logging:LogLevel:Default"];
            var level = Enum.Parse<LogEventLevel>(defaultLogLevel ?? "Information");

            if (level is LogEventLevel.Verbose or LogEventLevel.Debug or LogEventLevel.Information)
                Console.WriteLine($"## defaultLogLevel: '{defaultLogLevel}'");

            switch (level)
            {
                case LogEventLevel.Verbose:
                    config.MinimumLevel.Verbose();
                    break;
                case LogEventLevel.Debug:
                    config.MinimumLevel.Debug();
                    break;
                case LogEventLevel.Information:
                    config.MinimumLevel.Information();
                    break;
                case LogEventLevel.Warning:
                    config.MinimumLevel.Warning();
                    break;
                case LogEventLevel.Error:
                    config.MinimumLevel.Error();
                    break;
                case LogEventLevel.Fatal:
                    config.MinimumLevel.Fatal();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }

            return config.CreateLogger();
        }

        private static async Task StartServer(string[] args)
        {
            _builder = WebApplication.CreateBuilder(args);
            _mediatRWriter = new WrappingWriter(Console.Out);
            _appServices = Services(_builder, _mediatRWriter);
            _builder.Host.UseSerilog();
            _app = _builder.Build();

            if (!_app.Environment.IsDevelopment())
            {
                _app.UseExceptionHandler("/Error", true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                _app.UseHsts();
            }
            else
            {
                await MediatRExample(_appServices, _mediatRWriter);

                Log.Logger.Debug("## Debug");
                Log.Logger.Information("## Information");
                Log.Logger.Warning("## Warning");
                Log.Logger.Error("## Error");
            }

            _app.UseHttpsRedirection();
            _app.UseStaticFiles();
            _app.UseAntiforgery();
            _app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            await _app.RunAsync();

            return;

            IServiceCollection Services(WebApplicationBuilder webApplicationBuilder, WrappingWriter wrappingWriter)
            {
                // Add services to the container.
                var serviceCollection = webApplicationBuilder.Services;

                serviceCollection.AddRazorComponents()
                    .AddInteractiveServerComponents();

                serviceCollection.AddSingleton<TextWriter>(wrappingWriter);

                serviceCollection.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(Ping).Assembly, typeof(Sing).Assembly); });

                serviceCollection.AddScoped(typeof(IStreamRequestHandler<Sing, Song>), typeof(SingHandler));
                serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(GenericPipelineBehavior<,>));
                serviceCollection.AddScoped(typeof(IRequestPreProcessor<>), typeof(GenericRequestPreProcessor<>));
                serviceCollection.AddScoped(typeof(IRequestPostProcessor<,>), typeof(GenericRequestPostProcessor<,>));
                serviceCollection.AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(GenericStreamPipelineBehavior<,>));

                serviceCollection.AddFluxor(config => {
                    config
                        .ScanAssemblies(typeof(Program).Assembly)
                        .UseRouting()
                        //.UseReduxDevTools()
                        ;
                });

                return serviceCollection;
            }

            async Task MediatRExample(IServiceCollection services, WrappingWriter writer)
            {
                var provider = services.BuildServiceProvider();
                var mediator = provider.GetRequiredService<IMediator>();

                await Runner.Run(mediator, writer, "ASP.NET Core DI", true);

                await writer.DisposeAsync();
            }
        }
    }
}
