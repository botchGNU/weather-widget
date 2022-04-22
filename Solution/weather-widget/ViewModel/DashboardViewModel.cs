using System;
using System.Windows.Input;
using weather_widget.Command;
using weather_widget.Store;

namespace weather_widget.ViewModel
{
    class DashboardViewModel : ViewModelBase
    {
        public DashboardViewModel(NavigationStore navigationStore, Func<SettingsViewModel> createSettingsViewModel)
        {
            SettingsButtonCommand = new NavigateCommand(navigationStore, createSettingsViewModel);
            CloseButtonCommand = new ExitApplicationCommand();
        }


        #region commands
        public ICommand SettingsButtonCommand { get; } //Command in order to switch to Settings-View
        public ICommand CloseButtonCommand { get; } //Command in order to terminate entire application
        #endregion

        #region properties
        public string CurrentDate { get => DateTime.Now.ToString();}
        public string CurrentDay { get => DateTime.Now.DayOfWeek.ToString(); }
        public string CurrentLocation { get => "Entenhausen DE"; }
        public string CurrentTemperature { get => "22 C"; } //placeholder
        public string CurrentType { get => "Cloudy"; }  //placeholder

        #endregion
    }
}
