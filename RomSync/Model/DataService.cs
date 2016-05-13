using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RomSync.Model
{
    public class DataService : IDataService
    {
        public Task<IEnumerable<GameState>> GetGameListAsync()
        {
            // TODO: Inject real path here
            // TODO: Combine with file scanner to determine state
            return Task<IEnumerable<GameState>>.Factory.StartNew(() =>
                DatabaseParser.ParseFile(@"C:\git\personal\FB Alpha v0.2.97.38 (ClrMame Pro XML).dat").
                Select(_ => new GameState(_, currentState: SyncState.Unsynced)));
        }
    }
}