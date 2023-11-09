// ReSharper disable once CheckNamespace

namespace MediatR.Examples;

public class PingedHandler : INotificationHandler<Pinged>
{
	private readonly TextWriter _writer;

	public PingedHandler(TextWriter writer) => _writer = writer;

	public Task Handle(Pinged notification, CancellationToken cancellationToken) => _writer.WriteLineAsync("Got pinged async.");
}

public class PongedHandler : INotificationHandler<Ponged>
{
	private readonly TextWriter _writer;

	public PongedHandler(TextWriter writer) => _writer = writer;

	public Task Handle(Ponged notification, CancellationToken cancellationToken) => _writer.WriteLineAsync("Got ponged async.");
}

public class ConstrainedPingedHandler<TNotification> : INotificationHandler<TNotification>
	where TNotification : Pinged
{
	private readonly TextWriter _writer;

	public ConstrainedPingedHandler(TextWriter writer) => _writer = writer;

	public Task Handle(TNotification notification, CancellationToken cancellationToken) => _writer.WriteLineAsync("Got pinged constrained async.");
}

public class PingedAlsoHandler : INotificationHandler<Pinged>
{
	private readonly TextWriter _writer;

	public PingedAlsoHandler(TextWriter writer) => _writer = writer;

	public Task Handle(Pinged notification, CancellationToken cancellationToken) => _writer.WriteLineAsync("Got pinged also async.");
}
