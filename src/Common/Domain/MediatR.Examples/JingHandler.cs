// ReSharper disable once CheckNamespace
// ReSharper disable once IdentifierTypo

namespace MediatR.Examples;

public class JingHandler(TextWriter writer) : IRequestHandler<Jing>
{
	public Task Handle(Jing request, CancellationToken cancellationToken)
	{
		return writer.WriteLineAsync($"--- Handled Jing: {request.Message}, no Jong");
	}
}