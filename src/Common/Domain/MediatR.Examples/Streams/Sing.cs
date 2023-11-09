// ReSharper disable once CheckNamespace

namespace MediatR.Examples;

public class Sing : IStreamRequest<Song>
{
	public required string Message { get; set; }
}
