using System.Globalization;
using Serilog;

namespace Common.Store;

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
		private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

		[ReducerMethod, UsedImplicitly]
		public static State OnIncrementCounter(State state, IncrementCounter action)
		{
			Log.Information(string.Create(CultureInfo.InvariantCulture, $"Old state {state.ClickCount}"));

			return new State { ClickCount = state.ClickCount + action.Amount };
		}
	}
}