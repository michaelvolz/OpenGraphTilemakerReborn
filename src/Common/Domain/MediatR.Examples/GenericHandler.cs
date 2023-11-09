// ReSharper disable once CheckNamespace

namespace MediatR.Examples;

public class GenericHandler : INotificationHandler<INotification>
{
	private readonly TextWriter _writer;

	public GenericHandler(TextWriter writer) => _writer = writer;

	public Task Handle(INotification notification, CancellationToken cancellationToken) => _writer.WriteLineAsync("Got notified.");
}
