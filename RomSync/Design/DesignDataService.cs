using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RomSync.Model;

namespace RomSync.Design
{
    public class DesignDataService : IDataService
    {
        public Task<IEnumerable<GameState>> GetGameListAsync()
        {
            return Task.FromResult(new[]
            {
                new GameState(
                    new GameInfo("shortName","longName","Manufacturer","1981"),
                    SyncState.Unsynced),
            }.AsEnumerable());
        }
    }
}