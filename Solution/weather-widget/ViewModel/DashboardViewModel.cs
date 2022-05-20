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
            _weatherList.CollectionChanged += _weatherList_CollectionChanged;   //subscribe to collectionchanged event
            SettingsButtonCommand = new NavigateCommand(navigationStore, createSettingsViewModel);
            CloseButtonCommand = new ExitApplicationCommand();
        }
        #endregion

        #region commands
        public ICommand SettingsButtonCommand { get; } //Command in order to switch to Settings-View
        public ICommand CloseButtonCommand { get; } //Command in order to terminate entire application
        #endregion

        #region methods
        // update all bindings
        public void WeatherPropertyChanged()
        {
            OnPropertyChanged(nameof(CurrentLocation));
            OnPropertyChanged(nameof(CurrentDate));
            OnPropertyChanged(nameof(CurrentDay));
            OnPropertyChanged(nameof(Humidity));
            OnPropertyChanged(nameof(MinTemp));
            OnPropertyChanged(nameof(MaxTemp));
            OnPropertyChanged(nameof(AvTemp));
            OnPropertyChanged(nameof(ForecastList));
            OnPropertyChanged(nameof(WeatherImageSource));
        }
        #endregion

        #region events
        private void _weatherList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            WeatherPropertyChanged();   //update bindings
        }
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
                OnPropertyChanged(nameof(CurrentLocation));    
            }
        }
        public string Humidity 
        { 
            get 
            { 
                if (ForecastList.Count == 0)
                {
                    return "X";
                }
                else
                {
                    return Convert.ToString(ForecastList[0]?.Humidity);
                }
            } 
        }
        public string MinTemp 
        {
            get
            {
                if (ForecastList.Count == 0)
                {
                    return "X";
                }
                else
                {
                    return ForecastList[0]?.MinTemperature + " °C";
                }
            }
        }
        public string MaxTemp 
        {
            get
            {
                if (ForecastList.Count == 0)
                {
                    return "X";
                }
                else
                {
                    return ForecastList[0]?.MaxTemperature + " °C";
                }
            }
        }
        public string AvTemp 
        {
            get
            {
                if (ForecastList.Count == 0)
                {
                    return "X";
                }
                else
                {
                    return ForecastList[0]?.AvgTemperature + " °C";
                }
            }
        }
        public WeatherToDisplayListModel ForecastList { get => _weatherList; }   
        public BitmapImage WeatherImageSource
        {
            get
            {
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();

                if (ForecastList.Count == 0)
                {

                }
                else
                {
                    bi3.UriSource = new Uri(@"..\Resources\Icons\" + ForecastList[0].WeatherIcon, UriKind.Relative);
                }
                return bi3;
            }
        }

        #endregion
    }
}
