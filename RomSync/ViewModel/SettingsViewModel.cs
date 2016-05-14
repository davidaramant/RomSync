using System.ComponentModel;
using System.Runtime.CompilerServices;
using RomSync.Annotations;
using RomSync.Properties;

namespace RomSync.ViewModel
{
    public sealed class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly Settings _settings = Settings.Default;

        public string DatabaseFilePath
        {
            get { return _settings.DatabaseFilePath; }
            set
            {
                if (value != _settings.DatabaseFilePath)
                {
                    _settings.DatabaseFilePath = value;
                    _settings.Save();

                    OnPropertyChanged();
                }
            }
        }

        public string InputPath
        {
            get { return _settings.InputPath; }
            set
            {
                if (value != _settings.InputPath)
                {
                    _settings.InputPath = value;
                    _settings.Save();

                    OnPropertyChanged();
                }
            }
        }

        public string OutputPath
        {
            get { return _settings.OutputPath; }
            set
            {
                if (value != _settings.OutputPath)
                {
                    _settings.OutputPath = value;
                    _settings.Save();

                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
