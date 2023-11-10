using MediatR.Pipeline;

// ReSharper disable once CheckNamespace
namespace MediatR.Examples.ExceptionHandler;

#pragma warning disable MA0048 // File name must match type name
public class CommonExceptionHandler(TextWriter writer) : IRequestExceptionHandler<PingResource, Pong, Exception>
{
	public async Task Handle(PingResource request,
		Exception exception,
		RequestExceptionHandlerState<Pong> state,
		CancellationToken cancellationToken)
	{
		// Exception type name must be written in messages by LogExceptionAction before
		// Exception handler type name required because it is checked later in messages
		await writer.WriteLineAsync($"---- Exception Handler: '{typeof(CommonExceptionHandler).FullName}'").ConfigureAwait(false);

		state.SetHandled(new Pong());
	}
}

public class ConnectionExceptionHandler(TextWriter writer) : IRequestExceptionHandler<PingResource, Pong, ConnectionException>
{
	public async Task Handle(PingResource request,
		ConnectionException exception,
		RequestExceptionHandlerState<Pong> state,
		CancellationToken cancellationToken)
	{
		// Exception type name must be written in messages by LogExceptionAction before
		// Exception handler type name required because it is checked later in messages
		await writer.WriteLineAsync($"---- Exception Handler: '{typeof(ConnectionExceptionHandler).FullName}'").ConfigureAwait(false);

		state.SetHandled(new Pong());
	}
}

public class AccessDeniedExceptionHandler(TextWriter writer) : IRequestExceptionHandler<PingResource, Pong, ForbiddenException>
{
	public async Task Handle(PingResource request,
		ForbiddenException exception,
		RequestExceptionHandlerState<Pong> state,
		CancellationToken cancellationToken)
	{
		// Exception type name must be written in messages by LogExceptionAction before
		// Exception handler type name required because it is checked later in messages
		await writer.WriteLineAsync($"---- Exception Handler: '{typeof(AccessDeniedExceptionHandler).FullName}'").ConfigureAwait(false);

		state.SetHandled(new Pong());
	}
}

public class ServerExceptionHandler(TextWriter writer) : IRequestExceptionHandler<PingNewResource, Pong, ServerException>
{
	public virtual async Task Handle(PingNewResource request,
		ServerException exception,
		RequestExceptionHandlerState<Pong> state,
		CancellationToken cancellationToken)
	{
		// Exception type name must be written in messages by LogExceptionAction before
		// Exception handler type name required because it is checked later in messages
		await writer.WriteLineAsync($"---- Exception Handler: '{typeof(ServerExceptionHandler).FullName}'").ConfigureAwait(false);

		state.SetHandled(new Pong());
	}
}
