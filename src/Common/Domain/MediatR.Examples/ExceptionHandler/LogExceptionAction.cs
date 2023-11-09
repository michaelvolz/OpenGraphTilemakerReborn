using MediatR.Pipeline;

// ReSharper disable once CheckNamespace
namespace MediatR.Examples.ExceptionHandler;

public class LogExceptionAction : IRequestExceptionAction<Ping, Exception>
{
	private readonly TextWriter _writer;

	public LogExceptionAction(TextWriter writer) => _writer = writer;

	public Task Execute(Ping request, Exception exception, CancellationToken cancellationToken)
		=> _writer.WriteLineAsync($"--- Exception: '{exception.GetType().FullName}'");
}
