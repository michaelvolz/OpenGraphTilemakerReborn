using MediatR.Pipeline;

// ReSharper disable once CheckNamespace
namespace MediatR.Examples;

public class ConstrainedRequestPostProcessor<TRequest, TResponse>
	: IRequestPostProcessor<TRequest, TResponse>
	where TRequest : Ping
{
	private readonly TextWriter _writer;

	public ConstrainedRequestPostProcessor(TextWriter writer) => _writer = writer;

	public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken) => _writer.WriteLineAsync("- All Done with Ping");
}
