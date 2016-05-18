using System;
using System.Threading;
using System.Threading.Tasks;

namespace RomSync.ViewModel.Utilities
{
    public sealed class AsyncCommand<TResult> : AsyncCommandBase, IAsyncCommand<TResult>
    {
        private readonly Func<CancellationToken, Task<TResult>> _command;
        private NotifyTaskCompletion<TResult> _execution;

        public NotifyTaskCompletion<TResult> Execution

        {
            get { return _execution; }
            private set
            {
                if (Equals(value, _execution)) return;
                _execution = value;
                OnPropertyChanged();
            }
        }

        public AsyncCommand(Func<CancellationToken, Task<TResult>> command)
        {
            _command = command;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            NotifyCancelCommandThatCommandIsStarting();
            Execution = new NotifyTaskCompletion<TResult>(_command(CancellationToken));
            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            NotifyCancelCommandThatCommandIsFinished();
            RaiseCanExecuteChanged();
        }
    }
}
