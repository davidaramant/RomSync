using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        public int Year => _info.Year;

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
        }

        public GameViewModel(GameState gameState)
        {
            State = gameState.CurrentState;
            _info = gameState.Info;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
