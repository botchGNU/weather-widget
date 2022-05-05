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
        #region testing purpuse
        private WeatherInfoListModel _testingList = new WeatherInfoListModel();

        private void FillTestingList()
        {
            for (int i = 0; i < 5; i++)
            {
                var weatherNew = new WeatherInfoModel("Cloudy","01d",DateTime.Now,i+10,i,22, "NNW",i+5,50);
                weatherNew.WeatherIcon = "02n";
                _testingList.Add(weatherNew);
            }
            OnPropertyChanged(nameof(ForecastList));
        }
        #endregion

        #region ctor
        public DashboardViewModel(NavigationStore navigationStore, Func<SettingsViewModel> createSettingsViewModel)
        {
            SettingsButtonCommand = new NavigateCommand(navigationStore, createSettingsViewModel);
            CloseButtonCommand = new ExitApplicationCommand();
            FillTestingList();  //testing purpose
        }
        #endregion
        #region commands
        public ICommand SettingsButtonCommand { get; } //Command in order to switch to Settings-View
        public ICommand CloseButtonCommand { get; } //Command in order to terminate entire application
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
