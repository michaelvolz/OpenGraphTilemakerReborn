// ReSharper disable once CheckNamespace
// ReSharper disable once IdentifierTypo
// ReSharper disable UnusedMember.Global

namespace MediatR.Examples.ExceptionHandler;

#pragma warning disable MA0048 // File name must match type name
#pragma warning disable CS9113 // Parameter is unread.

public class PingResourceHandler(TextWriter writer) : IRequestHandler<PingResource, Pong>
{
	public Task<Pong> Handle(PingResource request, CancellationToken cancellationToken)
	{
		throw new ResourceNotFoundException();
	}
}

public class PingNewResourceHandler(TextWriter writer) : IRequestHandler<PingNewResource, Pong>
{
	public Task<Pong> Handle(PingNewResource request, CancellationToken cancellationToken)
	{
		throw new ServerException();
	}
}

public class PingResourceTimeoutHandler(TextWriter writer) : IRequestHandler<PingResourceTimeout, Pong>
{
	public Task<Pong> Handle(PingResourceTimeout request, CancellationToken cancellationToken)
	{
		throw new TaskCanceledException();
	}
}

public class PingResourceTimeoutOverrideHandler
	(TextWriter writer) : IRequestHandler<Overrides.PingResourceTimeout, Pong>
{
	public Task<Pong> Handle(Overrides.PingResourceTimeout request, CancellationToken cancellationToken)
	{
		throw new TaskCanceledException();
	}
}

public class PingProtectedResourceHandler(TextWriter writer) : IRequestHandler<PingProtectedResource, Pong>
{
	public Task<Pong> Handle(PingProtectedResource request, CancellationToken cancellationToken)
	{
		throw new ForbiddenException();
	}
}