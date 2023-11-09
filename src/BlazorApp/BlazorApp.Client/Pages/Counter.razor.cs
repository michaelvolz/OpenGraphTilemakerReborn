using Common.Store;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Serilog;

namespace BlazorApp.Client.Pages
{
	public partial class Counter : IDisposable
	{
		// [Inject] private ILogger<Counter> Logger { get; set; } = default!;
		[Inject] public IState<CounterRedux.State> CounterState { get; set; } = default!;
		[Inject] public IDispatcher Dispatcher { get; set; } = default!;
		[Inject] private IStore Store { get; set; } = default!;

		void IDisposable.Dispose() => CounterState.StateChanged -= CounterState_StateChanged;

		private void IncrementCount1()
		{
			Log.Information("IncrementCount1");

			Dispatcher.Dispatch(new CounterRedux.IncrementCounter());
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				Log.Information("### OnAfterRenderAsync");
				await Store.InitializeAsync();

				CounterState.StateChanged += CounterState_StateChanged;
			}
		}

		private void CounterState_StateChanged(object? sender, EventArgs e) => InvokeAsync(StateHasChanged);

		protected override void OnAfterRender(bool firstRender)
		{
			if (firstRender) Log.Information("### OnAfterRender");
		}

		private void SendMessageToConsoleNow()
		{
			Log.Information("SendMessageToConsoleNow");
			Dispatcher.Dispatch(new SendMessageRedux.SendMessage($"Hello from Fluxor {CounterState.Value.ClickCount}"));
		}

		private void ThrowException()
		{
			throw new NotImplementedException();
		}
	}
}
