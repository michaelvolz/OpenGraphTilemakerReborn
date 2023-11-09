

// ReSharper disable once CheckNamespace
namespace MediatR.Examples;

public class JingHandler : IRequestHandler<Jing>
{
	private readonly TextWriter _writer;

	public JingHandler(TextWriter writer) => _writer = writer;

	public Task Handle(Jing request, CancellationToken cancellationToken) => _writer.WriteLineAsync($"--- Handled Jing: {request.Message}, no Jong");
}
