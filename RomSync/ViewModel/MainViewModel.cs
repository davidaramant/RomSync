using System;
using System.Collections.Generic;
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
        private IFilter _filter = Filter.Empty;

        public ObservableCollection<GameViewModel> GameList { get; } = new ObservableCollection<GameViewModel>();
        public ICollectionView GameListView { get; }
        public IAsyncCommand LoadStateCommand { get; }
        public ICommand ClearGameFilter { get; }
        public IAsyncCommand SyncCommand { get; }

        private string _filterInput = String.Empty;
        public string FilterInput
        {
            get { return _filterInput; }
            set
            {
                if (value != _filterInput)
                {
                    Set(ref _filterInput, value);
                    UpdateFilter(value);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;

            GameListView = CollectionViewSource.GetDefaultView(GameList);
            GameListView.Filter = FilterGames;

            // TODO: This thing is messed up.  By default the Execution is null, which causes a bunch of bindings to screw up
            LoadStateCommand = AsyncCommand.Create(GetGames);

            ClearGameFilter = new RelayCommand(() => FilterInput = String.Empty);

            SyncCommand = AsyncCommand.Create(SyncGames);
        }

        private async Task GetGames(CancellationToken cts)
        {
            var listTask = _dataService.GetGameListAsync();

            await listTask;

            var viewModels = listTask.Result.OrderBy(game => game.Info.LongName).Select(_ => new GameViewModel(_));

            GameList.Clear();
            GameList.AddRange(viewModels);
        }

        private async Task SyncGames(CancellationToken cts)
        {
            var pending = GameList.Where(g => g.PendingChange);

            var gamesToSync =
                pending.Select(g => Tuple.Create(new GameState(g.Info,g.ActualState), g.RequestedState));

            await _dataService.UpdateGameList(gamesToSync);

            foreach (var p in pending)
            {
                p.ChangeApplied();
            }
        }

        private bool FilterGames(object item)
        {
            var gameVm = item as GameViewModel;

            return gameVm == null || _filter.Matches(gameVm.Metadata);
        }

        public void UpdateFilter(string filterInput)
        {
            _filter = Filter.Parse(filterInput);
            GameListView.Refresh();
        }
    }
}