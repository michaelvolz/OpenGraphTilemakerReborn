

// ReSharper disable once CheckNamespace
namespace MediatR.Examples;

public class JingHandler(TextWriter writer) : IRequestHandler<Jing>
{
	public Task Handle(Jing request, CancellationToken cancellationToken) => writer.WriteLineAsync($"--- Handled Jing: {request.Message}, no Jong");
}
