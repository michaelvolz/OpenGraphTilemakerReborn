// ReSharper disable once CheckNamespace

namespace MediatR.Examples;

public class PingHandler(TextWriter writer) : IRequestHandler<Ping, Pong>
{
	public async Task<Pong> Handle(Ping request, CancellationToken cancellationToken)
	{
		await writer.WriteLineAsync($"--- Handled Ping: {request.Message}").ConfigureAwait(false);
		return new Pong { Message = request.Message + " Pong" };
	}
}
