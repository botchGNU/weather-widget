using System;
using System.Windows.Input;
using weather_widget.Command;
using weather_widget.Store;

namespace weather_widget.ViewModel
{
    class SettingsViewModel : ViewModelBase
    {
        private string _currentLocation;    //currently selected Location in Settings-View

        #region ctor
        public SettingsViewModel(NavigationStore navigationStore, Func<DashboardViewModel> createDashboardViewModel)
        {
            BackToDashboardButtonCommand = new NavigateCommand(navigationStore, createDashboardViewModel);
            CloseButtonCommand = new ExitApplicationCommand();
        }
        #endregion

        #region commands
        public ICommand BackToDashboardButtonCommand { get; }    //Command in order to switch to Dashboard-View
        public ICommand CloseButtonCommand { get; } //command in order to close window
        #endregion

        #region properties
        public string CurrentLocation { get => _currentLocation; set => _currentLocation = value; }    //Binding for View -> gets/sets location for weather api
        #endregion
    }
}
