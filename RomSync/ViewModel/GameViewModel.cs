using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RomSync.Annotations;
using RomSync.Model;

namespace RomSync.ViewModel
{
    public sealed class GameViewModel : INotifyPropertyChanged
    {
        public readonly GameInfo Info;
        private SyncState _actualState;
        private SyncState _requestedState;
        private bool _pendingChange;

        public string Name => Info.LongName;
        public string Manufacturer => Info.Manufacturer;
        public string Year => Info.Year;
        public string Megabytes => $"{Info.SizeInBytes/1024d/1024d:##0.0} MB";
        public string Metadata { get; }

        public SyncState RequestedState
        {
            get { return _requestedState; }
            set
            {
                if (value == _requestedState) return;
                _requestedState = value;
                OnPropertyChanged();
                PendingChange = value != ActualState;
            }
        }

        public SyncState ActualState
        {
            get { return _actualState; }
            private set
            {
                if (value == _actualState) return;
                _actualState = value;
                OnPropertyChanged();
                RequestedState = value;
                PendingChange = value != ActualState;
            }
        }

        public bool PendingChange
        {
            get{return _pendingChange;}
            private set
            {
                if (value == _pendingChange) return;
                _pendingChange = value;
                OnPropertyChanged();
            }
        }

        public void ChangeApplied()
        {
            ActualState = RequestedState;
        }

        public GameViewModel(GameState gameState)
        {
            ActualState = gameState.CurrentState;
            Info = gameState.Info;
            Metadata = (Name + " " + Manufacturer + " " + Year).ToLowerInvariant();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
