using MediatR.Pipeline;

// ReSharper disable once CheckNamespace
// ReSharper disable once IdentifierTypo
namespace MediatR.Examples;

public class GenericRequestPreProcessor<TRequest>(TextWriter writer) : IRequestPreProcessor<TRequest>
	where TRequest : notnull
{
	public Task Process(TRequest request, CancellationToken cancellationToken)
	{
		return writer.WriteLineAsync("- Starting Up");
	}
}