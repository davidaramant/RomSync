using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RomSync.Extensions;
using RomSync.Model;

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
        public ICommand LoadStateCommand { get; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;

            LoadStateCommand = new RelayCommand(() => LoadList());
        }

        private async Task LoadList()
        {
            var listTask = _dataService.GetGameListAsync();

            await listTask;

            GameList.AddRange(listTask.Result.OrderBy(game => game.Info.LongName).Select(_ => new GameViewModel(_)));
        }
    }
}