using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using weather_widget.Model;

namespace weather_widget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WeatherInfoListModel weatherInfos;
        public MainWindow()
        {
            InitializeComponent();
            //List<WeatherInfoModel> weatherInfo = await APIManagerModel.GetWeather("Rankweil");

            //Task<WeatherInfoListModel> weatherInfos = APIManagerModel.GetWeather("Rankweil");
            GetWeather("Rankweil");

            Debug.WriteLine("Do Other stuff");
            Debug.WriteLine("Do Other stuff");
            Debug.WriteLine("Do Other stuff");
            Debug.WriteLine("Do Other stuff");
            /*
            for (int i = 0; i < int.MaxValue; i++)
            {
                Debug.WriteLine(weatherInfos.Count);
            }
            */
            /*
            Task<WeatherInfoListModel> TaskWeatherInfoListModel = 
            WeatherInfoListModel weatherInfos = await GetWeather("Rankweil");
            */
            
        }
        private async void GetWeather(string cityname)
        {
            APIManagerModel aPIManagerModel = new APIManagerModel();
            Task<WeatherInfoListModel> TaskweatherInfos = aPIManagerModel.GetWeather(cityname);

            weatherInfos = await TaskweatherInfos;
            Debug.WriteLine(weatherInfos.Count);

            /*
            Task<string> s = api.GetWeather("Rankweil");
            string ss = await s;
            Debug.WriteLine(ss);
            */
        }
    }


}
