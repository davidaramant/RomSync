using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace RomSync.ViewModel.Utilities
{
    public sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
    {
        public NotifyTaskCompletion(Task<TResult> task)
        {
            Task = task;
            if (!task.IsCompleted)
            {
                TaskCompletion = WatchTaskAsync(task);
            }
        }
        public Task TaskCompletion { get; }

        private async Task WatchTaskAsync(Task task)
        {
            try
            {
                await task;
            }
            catch
            {
                // Intentionally not doing anything here
            }
            var propertyChanged = PropertyChanged;
            if (propertyChanged == null)
                return;

            Action<string> notifyChanged = name => propertyChanged(this, new PropertyChangedEventArgs(name));

            notifyChanged(nameof(Status));
            notifyChanged(nameof(IsCompleted));
            notifyChanged(nameof(IsNotCompleted));
            if (task.IsCanceled)
            {
                notifyChanged(nameof(IsCanceled));
            }
            else if (task.IsFaulted)
            {
                notifyChanged(nameof(IsFaulted));
                notifyChanged(nameof(Exception));
                notifyChanged(nameof(InnerException));
                notifyChanged(nameof(ErrorMessage));
            }
            else
            {
                notifyChanged(nameof(IsSuccessfullyCompleted));
                notifyChanged(nameof(Result));
            }
        }
        public Task<TResult> Task { get; }
        public TResult Result => (Task.Status == TaskStatus.RanToCompletion) ? Task.Result : default(TResult);
        public TaskStatus Status => Task.Status;
        public bool IsCompleted => Task.IsCompleted;
        public bool IsNotCompleted => !Task.IsCompleted;
        public bool IsSuccessfullyCompleted => Task.Status == TaskStatus.RanToCompletion;
        public bool IsCanceled => Task.IsCanceled;
        public bool IsFaulted => Task.IsFaulted;
        public AggregateException Exception => Task.Exception;
        public Exception InnerException => Exception?.InnerException;
        public string ErrorMessage => InnerException?.Message;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
