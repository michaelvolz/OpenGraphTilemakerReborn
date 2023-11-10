using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace MediatR.Examples;

public class SingHandler(TextWriter writer) : IStreamRequestHandler<Sing, Song>
{
	public async IAsyncEnumerable<Song> Handle(Sing request, [EnumeratorCancellation] CancellationToken cancellationToken)
	{
		await writer.WriteLineAsync($"--- Handled Sing: {request.Message}, Song").ConfigureAwait(false);
		yield return await Task.Run(() => new Song { Message = request.Message + "ing do" }).ConfigureAwait(false);
		yield return await Task.Run(() => new Song { Message = request.Message + "ing re" }).ConfigureAwait(false);
		yield return await Task.Run(() => new Song { Message = request.Message + "ing mi" }).ConfigureAwait(false);
		yield return await Task.Run(() => new Song { Message = request.Message + "ing fa" }).ConfigureAwait(false);
		yield return await Task.Run(() => new Song { Message = request.Message + "ing so" }).ConfigureAwait(false);
		yield return await Task.Run(() => new Song { Message = request.Message + "ing la" }).ConfigureAwait(false);
		yield return await Task.Run(() => new Song { Message = request.Message + "ing ti" }).ConfigureAwait(false);
		yield return await Task.Run(() => new Song { Message = request.Message + "ing do" }).ConfigureAwait(false);
	}
}
