using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
// ReSharper disable once IdentifierTypo
namespace MediatR.Examples;

public class GenericStreamPipelineBehavior<TRequest, TResponse>
	(TextWriter writer) : IStreamPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
	public async IAsyncEnumerable<TResponse> Handle(TRequest request, StreamHandlerDelegate<TResponse> next,
		[EnumeratorCancellation] CancellationToken cancellationToken)
	{
		await writer.WriteLineAsync("-- Handling StreamRequest").ConfigureAwait(false);
		await foreach (var response in next().WithCancellation(cancellationToken).ConfigureAwait(false))
			yield return response;
		await writer.WriteLineAsync("-- Finished StreamRequest").ConfigureAwait(false);
	}
}