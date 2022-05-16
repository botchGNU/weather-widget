using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using weather_widget.Command;
using weather_widget.Model;
using weather_widget.Store;

namespace weather_widget.ViewModel
{
    class DashboardViewModel : ViewModelBase
    {
        #region fields
        private DataBaseUpdateManagerModel _updateMan;
        private WeatherToDisplayListModel _weatherList;
        #endregion

        #region ctor
        public DashboardViewModel(NavigationStore navigationStore, Func<SettingsViewModel> createSettingsViewModel, DataBaseUpdateManagerModel updateManager)
        {
            _updateMan = updateManager;
            _weatherList = updateManager.WeatherList;
            //_testingList = new WeatherInfoListModel();
            SettingsButtonCommand = new NavigateCommand(navigationStore, createSettingsViewModel);
            CloseButtonCommand = new ExitApplicationCommand();
            FillTestingList();  //testing purpose
        }
        #endregion

        #region commands
        public ICommand SettingsButtonCommand { get; } //Command in order to switch to Settings-View
        public ICommand CloseButtonCommand { get; } //Command in order to terminate entire application
        #endregion

        #region methods
        #region testing purpuse

        
        private void FillTestingList()
        {
            for (int i = 0; i < 5; i++)
            {
                var weatherNew = new WeatherToDisplay("Cloudy","01d", Convert.ToString(i+10), Convert.ToString(i), Convert.ToString(22), Convert.ToString(i + 5), Convert.ToString(50));
                weatherNew.WeatherIcon = "02n";
                _weatherList.Add(weatherNew);
            }
            OnPropertyChanged(nameof(ForecastList));
        }
        
        #endregion
        #endregion

        #region properties  
        //bindings for view <-> viewmodel
        public string CurrentDate 
        {
            get 
            { 
                return 
                    (
                     DateTime.Now.Day + "/" + 
                     DateTime.Now.Month + "/" +
                     DateTime.Now.Year
                    ); 
            }
                
        }
        public string CurrentDay { get => DateTime.Now.DayOfWeek.ToString(); }
        public string CurrentLocation 
        { 
            get => _updateMan.CurrentCity; 
            set 
            {
                 // = value;
                OnPropertyChanged(nameof(CurrentLocation));    
            }
        }
        public string Humidity { get => Convert.ToString(ForecastList[0].Humidity); }
        public string MinTemp { get => ForecastList[0].MinTemperature + " °C"; }
        public string MaxTemp { get => ForecastList[0].MaxTemperature + " °C"; }
        public string AvTemp { get => ForecastList[0].AvgTemperature + " °C"; }
        public WeatherToDisplayListModel ForecastList { get => _weatherList; }   
        public BitmapImage WeatherImageSource
        {
            get
            {
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(@"..\Resources\Icons\" + ForecastList[0].WeatherIcon + ".png", UriKind.Relative);
                bi3.EndInit();
                return bi3;
            }
        }

        #endregion
    }
}
