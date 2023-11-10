using MediatR.Pipeline;

// ReSharper disable once CheckNamespace
namespace MediatR.Examples.ExceptionHandler;

public class LogExceptionAction(TextWriter writer) : IRequestExceptionAction<Ping, Exception>
{
	public Task Execute(Ping request, Exception exception, CancellationToken cancellationToken)
		=> writer.WriteLineAsync($"--- Exception: '{exception.GetType().FullName}'");
}
