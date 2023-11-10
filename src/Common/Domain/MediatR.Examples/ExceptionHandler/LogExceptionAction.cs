using MediatR.Pipeline;

// ReSharper disable once CheckNamespace
// ReSharper disable once IdentifierTypo
namespace MediatR.Examples.ExceptionHandler;

public class LogExceptionAction(TextWriter writer) : IRequestExceptionAction<Ping, Exception>
{
	public Task Execute(Ping request, Exception exception, CancellationToken cancellationToken)
	{
		return writer.WriteLineAsync($"--- Exception: '{exception.GetType().FullName}'");
	}
}