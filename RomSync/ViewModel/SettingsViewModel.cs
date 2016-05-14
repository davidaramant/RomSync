using System;
using System.ComponentModel;
using System.Monad.Maybe;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
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

        public ICommand PickDatabaseFileCommand { get; private set; }
        public ICommand PickInputPathCommand { get; private set; }
        public ICommand PickOutputPathCommand { get; private set; }

        public SettingsViewModel()
        {
            PickDatabaseFileCommand = new RelayCommand(() => PickFilePath().Into(path => { DatabaseFilePath = path; }));
            PickInputPathCommand = new RelayCommand(() => PickFolderPath().Into(path => { InputPath = path; }));
            PickOutputPathCommand = new RelayCommand(() => PickFolderPath(allowNetworkPaths:true).Into(path => { OutputPath = path; }));
        }

        public IOption<string> PickFilePath()
        {
            using (var fileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return Option.SomeOrNone(fileDialog.FileName);
                }
                return Option.None<string>();
            }
        }

        public IOption<string> PickFolderPath(bool allowNetworkPaths = false)
        {
            using (var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog() )
            {
                if (allowNetworkPaths)
                {
                    folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
                }
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return Option.SomeOrNone(folderBrowserDialog.SelectedPath);
                }
                return Option.None<string>();
            }
        }
    }
}
