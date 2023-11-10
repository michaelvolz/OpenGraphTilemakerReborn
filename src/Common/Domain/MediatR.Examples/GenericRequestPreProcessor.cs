using MediatR.Pipeline;

// ReSharper disable once CheckNamespace
namespace MediatR.Examples;

public class GenericRequestPreProcessor<TRequest>(TextWriter writer) : IRequestPreProcessor<TRequest>
	where TRequest : notnull
{
	public Task Process(TRequest request, CancellationToken cancellationToken) => writer.WriteLineAsync("- Starting Up");
}
