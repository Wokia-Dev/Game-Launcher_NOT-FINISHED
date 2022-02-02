using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Launcher.Core;

namespace Game_Launcher.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand BibliotequeViewCommand { get; set; }

        public RelayCommand InstallerViewCommand { get; set; }

        public RelayCommand TelechargementViewCommand { get; set; }

        public RelayCommand ChatViewCommand { get; set; }

        public RelayCommand SettingViewCommand { get; set; }


        public HomeViewModel HomeVm { get; set; }

        public BibliotequeViewModel BibliotequeVm { get; set; }

        public InstallerViewModel InstallerVm { get; set; }

        public TelechargementViewModel TelechargementVm { get; set; }

        public ChatViewModel ChatVm { get; set; }

        public SettingViewModel SettingVm { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVm = new HomeViewModel();
            BibliotequeVm = new BibliotequeViewModel();
            InstallerVm = new InstallerViewModel();
            TelechargementVm = new TelechargementViewModel();
            ChatVm = new ChatViewModel();
            SettingVm = new SettingViewModel();

            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVm;
            });
            BibliotequeViewCommand = new RelayCommand(o =>
            {
                CurrentView = BibliotequeVm;
            });
            InstallerViewCommand = new RelayCommand(o =>
            {
                CurrentView = InstallerVm;
            });
            TelechargementViewCommand = new RelayCommand(o =>
            {
                CurrentView = TelechargementVm;
            });
            ChatViewCommand = new RelayCommand(o =>
            {
                CurrentView = ChatVm;
            });
            SettingViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingVm;
            });
        }

    }

}

