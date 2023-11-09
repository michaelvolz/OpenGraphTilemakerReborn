// ReSharper disable once CheckNamespace

namespace MediatR.Examples;

public class Sing : IStreamRequest<Song>
{
	public string Message { get; set; }
}
