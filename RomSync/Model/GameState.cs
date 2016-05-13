namespace RomSync.Model
{
    public sealed class GameState
    {
        public GameInfo Info { get; }
        public SyncState CurrentState { get; set; }

        public GameState(GameInfo info, SyncState currentState)
        {
            Info = info;
            CurrentState = currentState;
        }
    }
}
