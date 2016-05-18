using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using RomSync.Annotations;

namespace RomSync.ViewModel.Utilities
{
    public abstract class AsyncCommandBase : INotifyPropertyChanged, IDisposable
    {
        private readonly CancelAsyncCommand _cancelCommand = new CancelAsyncCommand();

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract Task ExecuteAsync(object parameter);
        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public ICommand CancelCommand => _cancelCommand;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cancelCommand.Dispose();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void NotifyCancelCommandThatCommandIsStarting()
        {
            _cancelCommand.NotifyCommandStarting();
        }

        protected void NotifyCancelCommandThatCommandIsFinished()
        {
            _cancelCommand.NotifyCommandFinished();
        }

        protected CancellationToken CancellationToken => _cancelCommand.Token;

        protected sealed class CancelAsyncCommand : ICommand, IDisposable
        {
            private CancellationTokenSource _cts = new CancellationTokenSource();
            private bool _commandExecuting;
            public CancellationToken Token => _cts.Token;

            public void NotifyCommandStarting()
            {
                _commandExecuting = true;
                if (!_cts.IsCancellationRequested)
                    return;
                _cts.Dispose();
                _cts = new CancellationTokenSource();
                RaiseCanExecuteChanged();
            }
            public void NotifyCommandFinished()
            {
                _commandExecuting = false;
                RaiseCanExecuteChanged();
            }
            bool ICommand.CanExecute(object parameter)
            {
                return _commandExecuting && !_cts.IsCancellationRequested;
            }
            void ICommand.Execute(object parameter)
            {
                _cts.Cancel();
                RaiseCanExecuteChanged();
            }

            private void RaiseCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }

            public event EventHandler CanExecuteChanged;
            public void Dispose()
            {
                _cts.Dispose();
            }
        }
    }
}
