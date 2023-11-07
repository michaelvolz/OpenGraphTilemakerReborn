using MediatR;
using MediatR.Examples;
using MediatR.Pipeline;
using OpenGraphTilemakerReborn.Components;

namespace OpenGraphTilemakerReborn;

internal class Program
{
    private static WebApplicationBuilder _builder = null!;
    private static WrappingWriter _mediatRWriter = null!;
    private static IServiceCollection _appServices = null!;
    private static WebApplication _app = null!;

    public static async Task Main(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);
        _mediatRWriter = new WrappingWriter(Console.Out);
        _appServices = Services(_builder, _mediatRWriter);
        _app = _builder.Build();

        // Configure the HTTP request pipeline.
        if (!_app.Environment.IsDevelopment())
        {
            _app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            _app.UseHsts();
        }

        await MediatRExample(_appServices, _mediatRWriter);

        _app.UseHttpsRedirection();
        _app.UseStaticFiles();
        _app.UseAntiforgery();
        _app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
        _app.Run();

        IServiceCollection Services(WebApplicationBuilder webApplicationBuilder, WrappingWriter wrappingWriter)
        {
            // Add services to the container.
            IServiceCollection serviceCollection = webApplicationBuilder.Services;

            serviceCollection.AddRazorComponents()
                .AddInteractiveServerComponents();

            serviceCollection.AddSingleton<TextWriter>(wrappingWriter);

            serviceCollection.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(Ping).Assembly, typeof(Sing).Assembly); });

            serviceCollection.AddScoped(typeof(IStreamRequestHandler<Sing, Song>), typeof(SingHandler));
            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(GenericPipelineBehavior<,>));
            serviceCollection.AddScoped(typeof(IRequestPreProcessor<>), typeof(GenericRequestPreProcessor<>));
            serviceCollection.AddScoped(typeof(IRequestPostProcessor<,>), typeof(GenericRequestPostProcessor<,>));
            serviceCollection.AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(GenericStreamPipelineBehavior<,>));

            return serviceCollection;
        }

        async Task MediatRExample(IServiceCollection services, WrappingWriter writer)
        {
            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            await Runner.Run(mediator, writer, "ASP.NET Core DI", testStreams: true);

            await writer.DisposeAsync();
        }
    }
}