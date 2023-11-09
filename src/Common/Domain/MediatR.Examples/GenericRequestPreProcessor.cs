using MediatR.Pipeline;

// ReSharper disable once CheckNamespace
namespace MediatR.Examples;

public class GenericRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
{
	private readonly TextWriter _writer;

	public GenericRequestPreProcessor(TextWriter writer) => _writer = writer;

	public Task Process(TRequest request, CancellationToken cancellationToken) => _writer.WriteLineAsync("- Starting Up");
}
