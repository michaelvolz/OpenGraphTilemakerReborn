using MediatR.Pipeline;

// ReSharper disable once CheckNamespace
// ReSharper disable once IdentifierTypo
namespace MediatR.Examples;

public class GenericRequestPostProcessor<TRequest, TResponse>
	(TextWriter writer) : IRequestPostProcessor<TRequest, TResponse>
	where TRequest : notnull
{
	public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
	{
		return writer.WriteLineAsync("- All Done");
	}
}