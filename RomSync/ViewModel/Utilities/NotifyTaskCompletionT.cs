using System.Threading.Tasks;

namespace RomSync.ViewModel.Utilities
{
    public sealed class NotifyTaskCompletion<TResult> : BaseNotifyTaskCompletion
    {
        public NotifyTaskCompletion(Task<TResult> task) : base(task)
        {
            Task = task;
        }

        protected override void AdditionalSuccessfulNotification()
        {
            OnPropertyChanged(nameof(Result));
        }

        public Task<TResult> Task { get; }
        public TResult Result => (Task.Status == TaskStatus.RanToCompletion) ? Task.Result : default(TResult);
    }
}
