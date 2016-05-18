namespace RomSync.ViewModel.Utilities
{
    public interface IAsyncCommand<TResult> : IAsyncCommand
    {
        NotifyTaskCompletion<TResult> Execution { get; }
    }
}
