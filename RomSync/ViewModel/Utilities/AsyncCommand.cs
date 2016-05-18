using System;
using System.Threading;
using System.Threading.Tasks;

namespace RomSync.ViewModel.Utilities
{
    public sealed class AsyncCommand : AsyncCommandBase, IAsyncCommand
    {
        #region Builders

        public static AsyncCommand<TResult> Create<TResult>(Func<CancellationToken, Task<TResult>> command)
        {
            return new AsyncCommand<TResult>(command);
        }

        public static AsyncCommand Create(Func<CancellationToken, Task>  command)
        {
            return new AsyncCommand(command);
        }

        #endregion

        private readonly Func<CancellationToken, Task> _command;
        private NotifyTaskCompletion _execution;

        public NotifyTaskCompletion Execution

        {
            get { return _execution; }
            private set
            {
                if (Equals(value, _execution)) return;
                _execution = value;
                OnPropertyChanged();
            }
        }

        public AsyncCommand(Func<CancellationToken, Task> command)
        {
            _command = command;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            NotifyCancelCommandThatCommandIsStarting();
            Execution = new NotifyTaskCompletion(_command(CancellationToken));
            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            NotifyCancelCommandThatCommandIsFinished();
            RaiseCanExecuteChanged();
        }
    }
}
