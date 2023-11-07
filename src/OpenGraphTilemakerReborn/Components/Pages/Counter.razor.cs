using Fluxor;
using Microsoft.AspNetCore.Components;
using OpenGraphTilemakerReborn.Store;

namespace OpenGraphTilemakerReborn.Components.Pages
{
    public partial class Counter : IDisposable
    {
        [Inject] public IState<CounterState> CounterState { get; set; } = default!;
        [Inject] public IDispatcher Dispatcher { get; set; } = default!;
        [Inject] private IStore Store { get; set; } = default!;

        void IDisposable.Dispose() => CounterState.StateChanged -= CounterState_StateChanged;

        private void IncrementCount()
        {
            Console.WriteLine("IncrementCount");

            Dispatcher.Dispatch(new IncrementCounterAction());
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Console.WriteLine("### OnAfterRenderAsync");
                await Store.InitializeAsync();

                CounterState.StateChanged += CounterState_StateChanged;
            }
        }

        private void CounterState_StateChanged(object? sender, EventArgs e) => InvokeAsync(StateHasChanged);

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender) Console.WriteLine("### OnAfterRender");
        }

        protected void SendMessageToConsoleNow()
        {
            Console.WriteLine("SendMessageToConsoleNow");
            Dispatcher.Dispatch(new SendMessageAction($"Hello from Fluxor {CounterState.Value.ClickCount}"));
        }
    }
}
