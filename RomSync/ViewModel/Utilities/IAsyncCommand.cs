using System.Threading.Tasks;
using System.Windows.Input;

namespace RomSync.ViewModel.Utilities
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
        NotifyTaskCompletion Execution { get; }
    }

    public interface IAsyncCommand<TResult> : ICommand
    {
        Task ExecuteAsync(object parameter);
        NotifyTaskCompletion<TResult> Execution { get; }
    }
}
