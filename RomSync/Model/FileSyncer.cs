using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomSync.Model
{
    public sealed class FileSyncer
    {


        public void ChangeState(GameState game, SyncState newState)
        {
            switch (newState)
            {
                case SyncState.Arcade:
                    switch (game.CurrentState)
                    {
                        case SyncState.Arcade:
                        case SyncState.NeoGeo:
                        case SyncState.Unsynced:
                        default:
                            break;
                    }
                    break;

                case SyncState.NeoGeo:
                    switch (game.CurrentState)
                    {
                        case SyncState.Arcade:
                        case SyncState.NeoGeo:
                        case SyncState.Unsynced:
                        default:
                            break;
                    }
                    break;

                case SyncState.Unsynced:
                    switch (game.CurrentState)
                    {
                        case SyncState.Arcade:
                        case SyncState.NeoGeo:
                        case SyncState.Unsynced:
                        default:
                            break;
                    }
                    break;

                default:
                    throw new NotSupportedException();
            }

            game.CurrentState = newState;
        }
    }
}
