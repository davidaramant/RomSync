using System.Threading.Tasks;

namespace RomSync.ViewModel.Utilities
{
    public sealed class NotifyTaskCompletion : BaseNotifyTaskCompletion
    {
        public NotifyTaskCompletion(Task task) : base(task)
        {
            Task = task;
        }

        public Task Task { get; }
    }
}
