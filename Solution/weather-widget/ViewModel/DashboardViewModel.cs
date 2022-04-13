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
            SettingsCommand = new NavigateCommand(navigationStore, createSettingsViewModel);
        }


        #region commands
        public ICommand SettingsCommand { get; }    //Command in order to switch to Settings-View
        #endregion
    }
}
