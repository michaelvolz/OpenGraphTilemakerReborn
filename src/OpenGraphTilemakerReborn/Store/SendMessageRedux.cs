using Fluxor;

namespace OpenGraphTilemakerReborn.Store
{
    [FeatureState]
    public record SendMessageState
    {
        public string LastMessage { get; set; } = "None yet";
    }

    public record SendMessageAction(string Message) { }

    public static class SendMessageStateReducers
    {
        [ReducerMethod]
        public static SendMessageState OnSendMessageAction(SendMessageState state, SendMessageAction action)
            => state with { LastMessage = action.Message };
    }

    public static class SendMessageStateEffects
    {
        [EffectMethod]
        public static Task OnSendMessageAction(SendMessageAction action, IDispatcher dispatcher)
        {
            Console.WriteLine(action.Message);

            return Task.CompletedTask;
        }
    }
}
