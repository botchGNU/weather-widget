using System;
using System.Windows.Input;
using weather_widget.Command;
using weather_widget.Model;
using weather_widget.Store;

namespace weather_widget.ViewModel
{
    class DashboardViewModel : ViewModelBase
    {
        private WeatherInfoListModel _testingList = new WeatherInfoListModel();

        private void FillTestingList()
        {
            for (int i = 0; i < 5; i++)
            {
                var weatherNew = new WeatherInfoModel("Cloudy","N.A.",DateTime.Now,i+10,i,22, "NNW",i+5,50);
                _testingList.Add(weatherNew);
            }
            OnPropertyChanged(nameof(ForecastList));
        }

        #region ctor
        public DashboardViewModel(NavigationStore navigationStore, Func<SettingsViewModel> createSettingsViewModel)
        {
            SettingsButtonCommand = new NavigateCommand(navigationStore, createSettingsViewModel);
            CloseButtonCommand = new ExitApplicationCommand();
            FillTestingList();
        }
        #endregion
        #region commands
        public ICommand SettingsButtonCommand { get; } //Command in order to switch to Settings-View
        public ICommand CloseButtonCommand { get; } //Command in order to terminate entire application
        #endregion

        #region properties
        public string CurrentDate { get => DateTime.Now.ToString();}
        public string CurrentDay { get => DateTime.Now.DayOfWeek.ToString(); }
        public string CurrentLocation 
        { 
            get => "Entenhausen DE"; 
            set 
            {
                 // = value;
                OnPropertyChanged(nameof(CurrentLocation));    
            }
        }
        public string CurrentTemperature { get => "22 C"; } //placeholder
        public string CurrentType { get => "Cloudy"; }  //placeholder
        public WeatherInfoListModel ForecastList { get => _testingList; }

        #endregion
    }
}
