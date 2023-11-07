using Fluxor;

namespace OpenGraphTilemakerReborn.Store
{
    [FeatureState]
    public class CounterState
    {
        private CounterState() { }

        public CounterState(int clickCount) => ClickCount = clickCount;

        public int ClickCount { get; }
    }

    public class IncrementCounterAction { }

    public static class CounterReducer
    {
        [ReducerMethod]
        public static CounterState OnIncrementCounterAction(CounterState state, IncrementCounterAction counterAction)
        {
            Console.WriteLine($"Old state {state.ClickCount}");

            return new CounterState(state.ClickCount + 1);
        }
    }
}
