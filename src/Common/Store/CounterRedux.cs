using Serilog;

namespace Common.Store;

public class CounterRedux
{
	public void Test()
	{
		using (var fs = new FileStream("path", FileMode.OpenOrCreate))
		{
			using (var sr = new StreamReader(fs))
			{
			}
		}
	}

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

		[ReducerMethod]
		[UsedImplicitly]
		public static State OnIncrementCounter(State state, IncrementCounter action)
		{
			Log.Information(FormattableString.Invariant($"Old state {state.ClickCount}"));

			return new State { ClickCount = state.ClickCount + action.Amount };
		}
	}
}
