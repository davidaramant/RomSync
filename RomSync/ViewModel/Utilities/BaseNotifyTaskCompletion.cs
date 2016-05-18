using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RomSync.Annotations;

namespace RomSync.ViewModel.Utilities
{
    public abstract class BaseNotifyTaskCompletion : INotifyPropertyChanged
    {
        protected BaseNotifyTaskCompletion(Task internalTask)
        {
            InternalTask = internalTask;
            if (!internalTask.IsCompleted)
            {
                TaskCompletion = WatchTaskAsync(internalTask);
            }
        }

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
                AdditionalSuccessfulNotification();
            }
        }

        protected virtual void AdditionalSuccessfulNotification() { }

        private Task InternalTask { get; }

        public Task TaskCompletion { get; }
        public TaskStatus Status => InternalTask.Status;
        public bool IsCompleted => InternalTask.IsCompleted;
        public bool IsNotCompleted => !InternalTask.IsCompleted;
        public bool IsSuccessfullyCompleted => InternalTask.Status == TaskStatus.RanToCompletion;
        public bool IsCanceled => InternalTask.IsCanceled;
        public bool IsFaulted => InternalTask.IsFaulted;
        public AggregateException Exception => InternalTask.Exception;
        public Exception InnerException => Exception?.InnerException;
        public string ErrorMessage => InnerException?.Message;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
