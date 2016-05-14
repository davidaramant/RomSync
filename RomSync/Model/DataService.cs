using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RomSync.Properties;

namespace RomSync.Model
{
    public class DataService : IDataService
    {
        public Task<IEnumerable<GameState>> GetGameListAsync()
        {
            // TODO: Combine with file scanner to determine state
            return Task<IEnumerable<GameState>>.Factory.StartNew(() =>
                DatabaseParser.ParseFile(Settings.Default.DatabaseFilePath).
                Select(_ => new GameState(_, currentState: SyncState.Unsynced)));
        }
    }
}