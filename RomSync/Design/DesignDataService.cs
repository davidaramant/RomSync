using System.Collections.Generic;
using System.Threading.Tasks;
using RomSync.Model;

namespace RomSync.Design
{
    public class DesignDataService : IDataService
    {
        public Task<IEnumerable<GameState>> GetGameListAsync()
        {
            return Task<IEnumerable<GameState>>.Factory.StartNew(() => new[]
            {
                new GameState(
                    new GameInfo("shortName","longName","Manufacturer","1981"),
                    SyncState.Unsynced),
            });
        }
    }
}