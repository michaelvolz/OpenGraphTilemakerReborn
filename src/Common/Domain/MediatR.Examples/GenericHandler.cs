// ReSharper disable once CheckNamespace

namespace MediatR.Examples;

public class GenericHandler(TextWriter writer) : INotificationHandler<INotification>
{
	public Task Handle(INotification notification, CancellationToken cancellationToken) => writer.WriteLineAsync("Got notified.");
}
