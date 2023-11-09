using Serilog;

namespace Common.Store
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
	            Log.Information($"Old state {state.ClickCount}");

                return new() { ClickCount = state.ClickCount + action.Amount };
            }
        }
    }
}
