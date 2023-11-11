using Serilog;

namespace Common.Store;

public static class SendMessageRedux
{
	[FeatureState]
	public record State
	{
		public string LastMessage { get; set; } = "None yet";
	}

	public record SendMessage(string Message);

	[UsedImplicitly]
	public static class Reducers
	{
		[ReducerMethod, UsedImplicitly]
#pragma warning disable RCS1163, IDE0060
		public static State OnSendMessage(State state, SendMessage sendMessage)
#pragma warning restore IDE0060, RCS1163
			=> new() { LastMessage = sendMessage.Message };
	}

	[UsedImplicitly]
	public static class Effects
	{
		private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

		[EffectMethod, UsedImplicitly]
#pragma warning disable RCS1163, IDE0060
		public static Task OnSendMessageAsync(SendMessage sendMessage, IDispatcher dispatcher)
#pragma warning restore IDE0060, RCS1163
		{
			Log.Information(sendMessage.Message);

			return Task.CompletedTask;
		}
	}
}