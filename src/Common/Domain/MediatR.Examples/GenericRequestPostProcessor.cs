using MediatR.Pipeline;

// ReSharper disable once CheckNamespace
namespace MediatR.Examples;

public class GenericRequestPostProcessor<TRequest, TResponse>
	(TextWriter writer) : IRequestPostProcessor<TRequest, TResponse>
	where TRequest : notnull
{
	public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken) => writer.WriteLineAsync("- All Done");
}
