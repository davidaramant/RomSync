using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RomSync.Annotations;
using RomSync.Model;

namespace RomSync.ViewModel
{
    public sealed class GameViewModel : INotifyPropertyChanged
    {
        private readonly GameInfo _info;
        private SyncState _actualState;
        private SyncState _requestedState;
        private bool _pendingChange;

        public string Name => _info.LongName;
        public string Manufacturer => _info.Manufacturer;
        public string Year => _info.Year;
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

        public GameViewModel(GameState gameState)
        {
            ActualState = gameState.CurrentState;
            _info = gameState.Info;
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
