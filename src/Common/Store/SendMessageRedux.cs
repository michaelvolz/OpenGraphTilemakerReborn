using Serilog;

namespace Common.Store
{
    public class SendMessageRedux
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
#pragma warning disable IDE0060
            public static State OnSendMessage(State state, SendMessage sendMessage)
	            => new() { LastMessage = sendMessage.Message };
        }

        [UsedImplicitly]
        public static class Effects
        {
	        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	        [EffectMethod, UsedImplicitly]
#pragma warning disable IDE0060
	        public static Task OnSendMessage(SendMessage sendMessage, IDispatcher dispatcher)
	        {
	            Log.Information(sendMessage.Message);

                return Task.CompletedTask;
            }
        }
    }
}
