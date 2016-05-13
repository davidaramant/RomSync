using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
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

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;

            LoadList();
        }

        private async Task LoadList()
        {
            var listTask = _dataService.GetGameListAsync();

            await listTask;

            foreach (var info in listTask.Result)
            {
                GameList.Add(new GameViewModel(info));
            }
        }
    }
}