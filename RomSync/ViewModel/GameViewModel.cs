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
        private SyncState _state;

        public string Name => _info.LongName;
        public string Manufacturer => _info.Manufacturer;
        public string Year => _info.Year;
        public string SearchString { get; }

        public SyncState State
        {
            get { return _state; }
            set
            {
                if (value == _state) return;
                _state = value;
                OnPropertyChanged();
            }
        }

        public GameViewModel(GameInfo info, SyncState state)
        {
            State = state;
            _info = info;
            SearchString = (Name + " " + Manufacturer + " " + Year).ToLowerInvariant();
        }

        public GameViewModel(GameState gameState) : this(gameState.Info, gameState.CurrentState)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
