using Fluxor;
using JetBrains.Annotations;

namespace OpenGraphTilemakerReborn.Store
{
    public class CounterRedux
    {
        [FeatureState]
        public record State
        {
            public int ClickCount { get; init; }
        }

        public record IncrementCounter(int Amount = 1);

        [UsedImplicitly]
        public static class Reducers
        {
            [ReducerMethod, UsedImplicitly]
            public static State OnIncrementCounter(State state, IncrementCounter action)
            {
                Console.WriteLine($"Old state {state.ClickCount}");

                return new() { ClickCount = state.ClickCount + action.Amount };
            }
        }
    }
}
