// ReSharper disable once CheckNamespace
// ReSharper disable once IdentifierTypo

namespace MediatR.Examples;

#pragma warning disable MA0048
public class PingedHandler(TextWriter writer) : INotificationHandler<Pinged>
{
	public Task Handle(Pinged notification, CancellationToken cancellationToken)
	{
		return writer.WriteLineAsync("Got pinged async.");
	}
}

public class PongedHandler(TextWriter writer) : INotificationHandler<Ponged>
{
	public Task Handle(Ponged notification, CancellationToken cancellationToken)
	{
		return writer.WriteLineAsync("Got ponged async.");
	}
}

public class ConstrainedPingedHandler<TNotification>(TextWriter writer) : INotificationHandler<TNotification>
	where TNotification : Pinged
{
	public Task Handle(TNotification notification, CancellationToken cancellationToken)
	{
		return writer.WriteLineAsync("Got pinged constrained async.");
	}
}

public class PingedAlsoHandler(TextWriter writer) : INotificationHandler<Pinged>
{
	public Task Handle(Pinged notification, CancellationToken cancellationToken)
	{
		return writer.WriteLineAsync("Got pinged also async.");
	}
}