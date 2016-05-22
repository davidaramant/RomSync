using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

        private readonly ObservableCollection<GameViewModel> _gameList = new ObservableCollection<GameViewModel>();
        private string _filter;
        public ICollectionView GameListView { get; }
        public IAsyncCommand LoadStateCommand { get; }
        public ICommand UpdateGameFilter { get; }

        public string Filter
        {
            get { return _filter; }
            set { Set(ref _filter, value); }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;

            GameListView = CollectionViewSource.GetDefaultView(_gameList);
            GameListView.Filter = FilterGames;

            // TODO: This thing is messed up.  By default the Execution is null, which causes a bunch of bindings to screw up
            LoadStateCommand = AsyncCommand.Create(GetGames);

            UpdateGameFilter = new RelayCommand(UpdateFilter);
        }

        private async Task GetGames(CancellationToken cts)
        {
            var listTask = _dataService.GetGameListAsync();

            await listTask;

            var viewModels = listTask.Result.OrderBy(game => game.Info.LongName).Select(_ => new GameViewModel(_));

            _gameList.Clear();
            _gameList.AddRange(viewModels);
        }

        private bool FilterGames(object item)
        {
            var gameVm = item as GameViewModel;

            if (!string.IsNullOrEmpty(Filter))
            {
                return gameVm.Name.ToLowerInvariant().Contains(Filter.ToLowerInvariant());
            }

            return true;
        }

        public void UpdateFilter()
        {
            GameListView.Refresh();
        }
    }
}