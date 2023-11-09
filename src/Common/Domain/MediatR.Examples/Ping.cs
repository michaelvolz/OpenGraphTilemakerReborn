// ReSharper disable once CheckNamespace

namespace MediatR.Examples;

public class Ping : IRequest<Pong>
{
	public string Message { get; set; } = string.Empty;
}
