// ReSharper disable once CheckNamespace
// ReSharper disable once IdentifierTypo

namespace MediatR.Examples;

public class GenericHandler(TextWriter writer) : INotificationHandler<INotification>
{
	public Task Handle(INotification notification, CancellationToken cancellationToken)
	{
		return writer.WriteLineAsync("Got notified.");
	}
}