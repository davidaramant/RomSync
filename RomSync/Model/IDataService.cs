using System.Collections.Generic;
using System.Threading.Tasks;

namespace RomSync.Model
{
    public interface IDataService
    {
        Task<IEnumerable<GameState>> GetGameListAsync();
    }
}
