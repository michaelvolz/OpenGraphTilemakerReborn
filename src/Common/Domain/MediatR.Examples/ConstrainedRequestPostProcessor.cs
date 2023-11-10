using MediatR.Pipeline;

// ReSharper disable once CheckNamespace
// ReSharper disable once IdentifierTypo
namespace MediatR.Examples;

public class ConstrainedRequestPostProcessor<TRequest, TResponse>
	(TextWriter writer) : IRequestPostProcessor<TRequest, TResponse> where TRequest : Ping
{
	public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
	{
		return writer.WriteLineAsync("- All Done with Ping");
	}
}