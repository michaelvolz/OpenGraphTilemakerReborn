using MediatR.Pipeline;

// ReSharper disable once CheckNamespace
namespace MediatR.Examples;

public class GenericRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TRequest : notnull
{
	private readonly TextWriter _writer;

	public GenericRequestPostProcessor(TextWriter writer) => _writer = writer;

	public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken) => _writer.WriteLineAsync("- All Done");
}
