using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using RomSync.Extensions;
using RomSync.Model;
using RomSync.ViewModel.Utilities;

namespace RomSync.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        public ObservableCollection<GameViewModel> GameList { get; } = new ObservableCollection<GameViewModel>();
        public IAsyncCommand<IEnumerable<GameViewModel>> LoadStateCommand { get; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;

            LoadStateCommand = AsyncCommand.Create(GetGames);
        }

        private async Task<IEnumerable<GameViewModel>> GetGames(CancellationToken cts)
        {
            var listTask = _dataService.GetGameListAsync();

            await listTask;

            var viewModels = listTask.Result.OrderBy(game => game.Info.LongName).Select(_ => new GameViewModel(_));

            GameList.Clear();
            GameList.AddRange(viewModels);

            // TODO: This is stupid...
            // TODO: Looks like it needs an untyped NotifyTaskCompletion and AsyncCommand for this to work the way it's supposed to.
            return viewModels;
        }
    }
}