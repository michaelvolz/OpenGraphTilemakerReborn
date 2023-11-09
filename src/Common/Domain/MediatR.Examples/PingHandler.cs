// ReSharper disable once CheckNamespace

namespace MediatR.Examples;

public class PingHandler : IRequestHandler<Ping, Pong>
{
	private readonly TextWriter _writer;

	public PingHandler(TextWriter writer) => _writer = writer;

	public async Task<Pong> Handle(Ping request, CancellationToken cancellationToken)
	{
		await _writer.WriteLineAsync($"--- Handled Ping: {request.Message}");
		return new Pong { Message = request.Message + " Pong" };
	}
}
