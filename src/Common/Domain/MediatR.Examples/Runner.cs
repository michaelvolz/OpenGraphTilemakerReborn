using System.Text;
using MediatR.Examples.ExceptionHandler;
using ServerExceptionHandler = MediatR.Examples.ExceptionHandler.Overrides.ServerExceptionHandler;

// ReSharper disable once CheckNamespace
// ReSharper disable once IdentifierTypo
namespace MediatR.Examples;

public static class Runner
{
#pragma warning disable IDE0300
	private static readonly string[] Separator = { "\r\n", "\r", "\n" };

#pragma warning disable MA0051
	public static async Task Run(IMediator mediator, WrappingWriter writer, string projectName,
		bool testStreams = false)
	{
		await writer.WriteLineAsync("===============").ConfigureAwait(false);
		await writer.WriteLineAsync(projectName).ConfigureAwait(false);
		await writer.WriteLineAsync("===============").ConfigureAwait(false);
		await writer.WriteLineAsync().ConfigureAwait(false);

		await writer.WriteLineAsync("Sending Ping...").ConfigureAwait(false);
		var pong = await mediator.Send(new Ping { Message = "Ping" }).ConfigureAwait(false);
		await writer.WriteLineAsync("Received: " + pong.Message).ConfigureAwait(false);
		await writer.WriteLineAsync().ConfigureAwait(false);

		await writer.WriteLineAsync("Publishing Pinged...").ConfigureAwait(false);
		await mediator.Publish(new Pinged()).ConfigureAwait(false);
		await writer.WriteLineAsync().ConfigureAwait(false);

		await writer.WriteLineAsync("Publishing Ponged...").ConfigureAwait(false);
		var failedPong = false;
		try
		{
			await mediator.Publish(new Ponged()).ConfigureAwait(false);
		}
		catch (Exception e)
		{
			failedPong = true;
			await writer.WriteLineAsync(e.ToString()).ConfigureAwait(false);
		}

		await writer.WriteLineAsync().ConfigureAwait(false);

		var failedJing = false;
		await writer.WriteLineAsync("Sending Jing...").ConfigureAwait(false);
		try
		{
			await mediator.Send(new Jing { Message = "Jing" }).ConfigureAwait(false);
		}
		catch (Exception e)
		{
			failedJing = true;
			await writer.WriteLineAsync(e.ToString()).ConfigureAwait(false);
		}

		await writer.WriteLineAsync().ConfigureAwait(false);

		var failedSing = false;
		if (testStreams)
		{
			await writer.WriteLineAsync("Sending Sing...").ConfigureAwait(false);
			try
			{
				var i = 0;
				await foreach (var s in mediator.CreateStream(new Sing { Message = "Sing" }).ConfigureAwait(false))
				{
					failedSing = i switch
					{
						0 => !s.Message.Contains("Singing do", StringComparison.OrdinalIgnoreCase),
						1 => !s.Message.Contains("Singing re", StringComparison.OrdinalIgnoreCase),
						2 => !s.Message.Contains("Singing mi", StringComparison.OrdinalIgnoreCase),
						3 => !s.Message.Contains("Singing fa", StringComparison.OrdinalIgnoreCase),
						4 => !s.Message.Contains("Singing so", StringComparison.OrdinalIgnoreCase),
						5 => !s.Message.Contains("Singing la", StringComparison.OrdinalIgnoreCase),
						6 => !s.Message.Contains("Singing ti", StringComparison.OrdinalIgnoreCase),
						7 => !s.Message.Contains("Singing do", StringComparison.OrdinalIgnoreCase),
						_ => failedSing,
					};

					failedSing = failedSing || ++i > 10;
				}
			}
			catch (Exception e)
			{
				failedSing = true;
				await writer.WriteLineAsync(e.ToString()).ConfigureAwait(false);
			}

			await writer.WriteLineAsync().ConfigureAwait(false);
		}

		var isHandlerForSameExceptionWorks =
			await IsHandlerForSameExceptionWorks(mediator, writer).ConfigureAwait(false);
		var isHandlerForBaseExceptionWorks =
			await IsHandlerForBaseExceptionWorks(mediator, writer).ConfigureAwait(false);
		var isHandlerForLessSpecificExceptionWorks =
			await IsHandlerForLessSpecificExceptionWorks(mediator, writer).ConfigureAwait(false);
		var isPreferredHandlerForBaseExceptionWorks =
			await IsPreferredHandlerForBaseExceptionWorks(mediator, writer).ConfigureAwait(false);
		var isOverriddenHandlerForBaseExceptionWorks =
			await IsOverriddenHandlerForBaseExceptionWorks(mediator, writer).ConfigureAwait(false);

		await writer.WriteLineAsync("---------------").ConfigureAwait(false);
		var contents = writer.Contents;
		var order = new[]
		{
			contents.IndexOf("- Starting Up", StringComparison.OrdinalIgnoreCase),
			contents.IndexOf("-- Handling Request", StringComparison.OrdinalIgnoreCase),
			contents.IndexOf("--- Handled Ping", StringComparison.OrdinalIgnoreCase),
			contents.IndexOf("-- Finished Request", StringComparison.OrdinalIgnoreCase),
			contents.IndexOf("- All Done", StringComparison.OrdinalIgnoreCase),
			contents.IndexOf("- All Done with Ping", StringComparison.OrdinalIgnoreCase),
		};

		var streamOrder = new[]
		{
			contents.IndexOf("-- Handling StreamRequest", StringComparison.OrdinalIgnoreCase),
			contents.IndexOf("--- Handled Sing: Sing, Song", StringComparison.OrdinalIgnoreCase),
			contents.IndexOf("-- Finished StreamRequest", StringComparison.OrdinalIgnoreCase),
		};

		var results = new RunResults
		{
			RequestHandlers = contents.Contains("--- Handled Ping:", StringComparison.OrdinalIgnoreCase),
			VoidRequestsHandlers = contents.Contains("--- Handled Jing:", StringComparison.OrdinalIgnoreCase),
			PipelineBehaviors = contents.Contains("-- Handling Request", StringComparison.OrdinalIgnoreCase),
			RequestPreProcessors = contents.Contains("- Starting Up", StringComparison.OrdinalIgnoreCase),
			RequestPostProcessors = contents.Contains("- All Done", StringComparison.OrdinalIgnoreCase),
			ConstrainedGenericBehaviors =
				contents.Contains("- All Done with Ping", StringComparison.OrdinalIgnoreCase) && !failedJing,
			OrderedPipelineBehaviors = order.SequenceEqual(order.OrderBy(i => i)),
			NotificationHandler = contents.Contains("Got pinged async", StringComparison.OrdinalIgnoreCase),
			MultipleNotificationHandlers =
				contents.Contains("Got pinged async", StringComparison.OrdinalIgnoreCase) &&
				contents.Contains("Got pinged also async", StringComparison.OrdinalIgnoreCase),
			ConstrainedGenericNotificationHandler =
				contents.Contains("Got pinged constrained async", StringComparison.OrdinalIgnoreCase) && !failedPong,
			CovariantNotificationHandler = contents.Contains("Got notified", StringComparison.OrdinalIgnoreCase),
			HandlerForSameException = isHandlerForSameExceptionWorks,
			HandlerForBaseException = isHandlerForBaseExceptionWorks,
			HandlerForLessSpecificException = isHandlerForLessSpecificExceptionWorks,
			PreferredHandlerForBaseException = isPreferredHandlerForBaseExceptionWorks,
			OverriddenHandlerForBaseException = isOverriddenHandlerForBaseExceptionWorks,

			// Streams
			StreamRequestHandlers =
				contents.Contains("--- Handled Sing: Sing, Song", StringComparison.OrdinalIgnoreCase) && !failedSing,
			StreamPipelineBehaviors =
				contents.Contains("-- Handling StreamRequest", StringComparison.OrdinalIgnoreCase),
			StreamOrderedPipelineBehaviors = streamOrder.SequenceEqual(streamOrder.OrderBy(i => i)),
		};

		await writer
			.WriteLineAsync(
				$"Request Handler....................................................{(results.RequestHandlers ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer
			.WriteLineAsync(
				$"Void Request Handler...............................................{(results.VoidRequestsHandlers ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer
			.WriteLineAsync(
				$"Pipeline Behavior..................................................{(results.PipelineBehaviors ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer
			.WriteLineAsync(
				$"Pre-Processor......................................................{(results.RequestPreProcessors ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer
			.WriteLineAsync(
				$"Post-Processor.....................................................{(results.RequestPostProcessors ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer
			.WriteLineAsync(
				$"Constrained Post-Processor.........................................{(results.ConstrainedGenericBehaviors ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer
			.WriteLineAsync(
				$"Ordered Behaviors..................................................{(results.OrderedPipelineBehaviors ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer
			.WriteLineAsync(
				$"Notification Handler...............................................{(results.NotificationHandler ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer
			.WriteLineAsync(
				$"Notification Handlers..............................................{(results.MultipleNotificationHandlers ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer.WriteLineAsync(
				$"Constrained Notification Handler...................................{(results.ConstrainedGenericNotificationHandler ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer
			.WriteLineAsync(
				$"Covariant Notification Handler.....................................{(results.CovariantNotificationHandler ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer
			.WriteLineAsync(
				$"Handler for inherited request with same exception used.............{(results.HandlerForSameException ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer
			.WriteLineAsync(
				$"Handler for inherited request with base exception used.............{(results.HandlerForBaseException ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer.WriteLineAsync(
				$"Handler for request with less specific exception used by priority..{(results.HandlerForLessSpecificException ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer.WriteLineAsync(
				$"Preferred handler for inherited request with base exception used...{(results.PreferredHandlerForBaseException ? "Y" : "N")}")
			.ConfigureAwait(false);
		await writer.WriteLineAsync(
				$"Overridden handler for inherited request with same exception used..{(results.OverriddenHandlerForBaseException ? "Y" : "N")}")
			.ConfigureAwait(false);

		if (testStreams)
		{
			await writer
				.WriteLineAsync(
					$"Stream Request Handler.............................................{(results.StreamRequestHandlers ? "Y" : "N")}")
				.ConfigureAwait(false);
			await writer
				.WriteLineAsync(
					$"Stream Pipeline Behavior...........................................{(results.StreamPipelineBehaviors ? "Y" : "N")}")
				.ConfigureAwait(false);
			await writer.WriteLineAsync(
					$"Stream Ordered Behaviors...........................................{(results.StreamOrderedPipelineBehaviors ? "Y" : "N")}")
				.ConfigureAwait(false);
		}

		await writer.WriteLineAsync().ConfigureAwait(false);
	}

	private static async Task<bool> IsHandlerForSameExceptionWorks(IMediator mediator, WrappingWriter writer)
	{
		var isHandledCorrectly = false;

		await writer.WriteLineAsync("Checking handler to catch exact exception...").ConfigureAwait(false);
		try
		{
			await mediator.Send(new PingProtectedResource { Message = "Ping to protected resource" })
				.ConfigureAwait(false);
			isHandledCorrectly = IsExceptionHandledBy<ForbiddenException, AccessDeniedExceptionHandler>(writer);
		}
		catch (Exception e)
		{
			await writer.WriteLineAsync(e.Message).ConfigureAwait(false);
		}

		await writer.WriteLineAsync().ConfigureAwait(false);

		return isHandledCorrectly;
	}

	private static async Task<bool> IsHandlerForBaseExceptionWorks(IMediator mediator, WrappingWriter writer)
	{
		var isHandledCorrectly = false;

		await writer.WriteLineAsync("Checking shared handler to catch exception by base type...").ConfigureAwait(false);
		try
		{
			await mediator.Send(new PingResource { Message = "Ping to missed resource" }).ConfigureAwait(false);
			isHandledCorrectly = IsExceptionHandledBy<ResourceNotFoundException, ConnectionExceptionHandler>(writer);
		}
		catch (Exception e)
		{
			await writer.WriteLineAsync(e.Message).ConfigureAwait(false);
		}

		await writer.WriteLineAsync().ConfigureAwait(false);

		return isHandledCorrectly;
	}

	private static async Task<bool> IsHandlerForLessSpecificExceptionWorks(IMediator mediator, WrappingWriter writer)
	{
		var isHandledCorrectly = false;

		await writer.WriteLineAsync("Checking base handler to catch any exception...").ConfigureAwait(false);
		try
		{
			await mediator.Send(new PingResourceTimeout { Message = "Ping to ISS resource" }).ConfigureAwait(false);
			isHandledCorrectly = IsExceptionHandledBy<TaskCanceledException, CommonExceptionHandler>(writer);
		}
		catch (Exception e)
		{
			await writer.WriteLineAsync(e.Message).ConfigureAwait(false);
		}

		await writer.WriteLineAsync().ConfigureAwait(false);

		return isHandledCorrectly;
	}

	private static async Task<bool> IsPreferredHandlerForBaseExceptionWorks(IMediator mediator, WrappingWriter writer)
	{
		var isHandledCorrectly = false;

		await writer.WriteLineAsync("Selecting preferred handler to handle exception...").ConfigureAwait(false);

		try
		{
			await mediator.Send(new ExceptionHandler.Overrides.PingResourceTimeout
				{ Message = "Ping to ISS resource (preferred)" }).ConfigureAwait(false);
			isHandledCorrectly =
				IsExceptionHandledBy<TaskCanceledException, ExceptionHandler.Overrides.CommonExceptionHandler>(writer);
		}
		catch (Exception e)
		{
			await writer.WriteLineAsync(e.Message).ConfigureAwait(false);
		}

		await writer.WriteLineAsync().ConfigureAwait(false);

		return isHandledCorrectly;
	}

	private static async Task<bool> IsOverriddenHandlerForBaseExceptionWorks(IMediator mediator, WrappingWriter writer)
	{
		var isHandledCorrectly = false;

		await writer.WriteLineAsync("Selecting new handler to handle exception...").ConfigureAwait(false);

		try
		{
			await mediator.Send(new PingNewResource { Message = "Ping to ISS resource (override)" })
				.ConfigureAwait(false);
			isHandledCorrectly = IsExceptionHandledBy<ServerException, ServerExceptionHandler>(writer);
		}
		catch (Exception e)
		{
			await writer.WriteLineAsync(e.Message).ConfigureAwait(false);
		}

		await writer.WriteLineAsync().ConfigureAwait(false);

		return isHandledCorrectly;
	}

	private static bool IsExceptionHandledBy<TException, THandler>(WrappingWriter writer)
		where TException : Exception
	{
		var messages = writer.Contents.Split(Separator, StringSplitOptions.None).ToList();
		if (messages.Count - 3 < 0)
			return false;

		// Note: For this handler type to be found in messages, it must be written in messages by LogExceptionAction
		return messages[^2].Contains(typeof(THandler).FullName ?? string.Empty, StringComparison.OrdinalIgnoreCase)

		       // Note: For this exception type to be found in messages, exception must be written in all tested exception handlers
		       && messages[^3].Contains(typeof(TException).FullName ?? string.Empty, StringComparison.OrdinalIgnoreCase);
	}
}

#pragma warning disable MA0048
public class RunResults
{
	public bool RequestHandlers { get; set; }
	public bool VoidRequestsHandlers { get; set; }
	public bool PipelineBehaviors { get; set; }
	public bool RequestPreProcessors { get; set; }
	public bool RequestPostProcessors { get; set; }
	public bool OrderedPipelineBehaviors { get; set; }
	public bool ConstrainedGenericBehaviors { get; set; }
	public bool NotificationHandler { get; set; }
	public bool MultipleNotificationHandlers { get; set; }
	public bool CovariantNotificationHandler { get; set; }
	public bool ConstrainedGenericNotificationHandler { get; set; }
	public bool HandlerForSameException { get; set; }
	public bool HandlerForBaseException { get; set; }
	public bool HandlerForLessSpecificException { get; set; }
	public bool PreferredHandlerForBaseException { get; set; }
	public bool OverriddenHandlerForBaseException { get; set; }

	// Stream results
	public bool StreamRequestHandlers { get; set; }
	public bool StreamPipelineBehaviors { get; set; }
	public bool StreamOrderedPipelineBehaviors { get; set; }
}

public class WrappingWriter(TextWriter innerWriter) : TextWriter
{
	private readonly StringBuilder _stringWriter = new();

	public override Encoding Encoding => innerWriter.Encoding;

	public string Contents => _stringWriter.ToString();

	public override void Write(char value)
	{
		_stringWriter.Append(value);
		innerWriter.Write(value);
	}

	public override Task WriteLineAsync(string? value)
	{
		_stringWriter.AppendLine(value);
		return innerWriter.WriteLineAsync(value);
	}
}