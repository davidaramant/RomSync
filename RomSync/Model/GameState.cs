using System.IO;
using RomSync.Properties;

namespace RomSync.Model
{
    public sealed class GameState
    {
        public GameInfo Info { get; }
        public SyncState CurrentState { get; set; }

        public string CurrentPath => Path.Combine(Settings.Default.GetPath(CurrentState), Info.FileName);

        public GameState(GameInfo info, SyncState currentState)
        {
            Info = info;
            CurrentState = currentState;
        }
    }
}
