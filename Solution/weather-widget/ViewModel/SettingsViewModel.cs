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
            BackCommand = new NavigateCommand(navigationStore, createDashboardViewModel);
        }
        #endregion

        #region commands
        public ICommand BackCommand { get; }    //Command in order to switch to Dashboard-View
        #endregion

        #region properties
        public string LocationText { get => _currentLocation; set => _currentLocation = value; }    //Binding for View
        #endregion
    }
}
