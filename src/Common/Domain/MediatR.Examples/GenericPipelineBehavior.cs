// ReSharper disable once CheckNamespace

namespace MediatR.Examples;

public class GenericPipelineBehavior<TRequest, TResponse>(TextWriter writer) : IPipelineBehavior<TRequest, TResponse>
	where TRequest : notnull
{
	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		await writer.WriteLineAsync("-- Handling Request").ConfigureAwait(false);
		var response = await next().ConfigureAwait(false);
		await writer.WriteLineAsync("-- Finished Request").ConfigureAwait(false);
		return response;
	}
}
