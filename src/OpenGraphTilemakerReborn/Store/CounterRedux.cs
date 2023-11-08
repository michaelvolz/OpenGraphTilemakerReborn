using Fluxor;

namespace OpenGraphTilemakerReborn.Store
{
    [FeatureState]
    public record CounterState
    {
        public int ClickCount { get; init; }
    }

    public record IncrementCounterAction(int Amount = 1) { }

    public static class CounterReducer
    {
        [ReducerMethod]
        public static CounterState OnIncrementCounterAction(CounterState state, IncrementCounterAction action)
        {
            Console.WriteLine($"Old state {state.ClickCount}");

            return state with { ClickCount = state.ClickCount + action.Amount };
        }
    }
}
