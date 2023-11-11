using MediatR.Pipeline;

// ReSharper disable CheckNamespace

namespace MediatR.Examples.ExceptionHandler.Overrides;

#pragma warning disable MA0048 // File name must match type name
public class CommonExceptionHandler(TextWriter writer) : IRequestExceptionHandler<PingResourceTimeout, Pong, Exception>
{
	public async Task Handle(PingResourceTimeout request, Exception exception, RequestExceptionHandlerState<Pong> state, CancellationToken cancellationToken)
	{
		// Exception type name must be written in messages by LogExceptionAction before
		// Exception handler type name required because it is checked later in messages
		await writer.WriteLineAsync($"---- Exception Handler: '{typeof(CommonExceptionHandler).FullName}'")
			.ConfigureAwait(false);

		state.SetHandled(new Pong());
	}
}

#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
public class ServerExceptionHandler(TextWriter writer) : ExceptionHandler.ServerExceptionHandler(writer)
{
	public override async Task Handle(PingNewResource request, ServerException exception, RequestExceptionHandlerState<Pong> state,
		CancellationToken cancellationToken)
	{
		// Exception type name must be written in messages by LogExceptionAction before
		// Exception handler type name required because it is checked later in messages
		await writer.WriteLineAsync($"---- Exception Handler: '{typeof(ServerExceptionHandler).FullName}'")
			.ConfigureAwait(false);

		state.SetHandled(new Pong());
	}
}