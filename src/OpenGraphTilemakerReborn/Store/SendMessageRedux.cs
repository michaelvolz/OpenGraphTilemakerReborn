using Fluxor;
using JetBrains.Annotations;

namespace OpenGraphTilemakerReborn.Store
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
            public static State OnSendMessage(State state, SendMessage sendMessage)
                => new() { LastMessage = sendMessage.Message };
        }

        [UsedImplicitly]
        public static class Effects
        {
            [EffectMethod, UsedImplicitly]
            public static Task OnSendMessage(SendMessage sendMessage, IDispatcher dispatcher)
            {
                Console.WriteLine(sendMessage.Message);

                return Task.CompletedTask;
            }
        }
    }
}
