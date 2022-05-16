﻿using System;
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
            weatherInfos = new WeatherInfoListModel();
            DataBaseManagerModel dataBaseManagerModel = new DataBaseManagerModel();
            List<string> cities = dataBaseManagerModel.GetCitiesByLetters("Rankwe");
            foreach (string item in cities)
            {
                Debug.WriteLine(item);
            }


            //dataBaseManagerModel.GetDataFromOpenWeather("Rankweil");
            
            dataBaseManagerModel.GetDataFromOpenWeather("London");

            Debug.WriteLine("Do other stuff");
            Debug.WriteLine("Do other stuff");
            Debug.WriteLine("Do other stuff");
        }

        /*
        private async void GetWeather(string cityname)
        {
            APIManagerModel apimanagerModel = new APIManagerModel();
            Task<WeatherInfoListModel> TaskweatherInfos = apimanagerModel.GetWeather(cityname);

            weatherInfos = await TaskweatherInfos;
            Debug.WriteLine(weatherInfos.Count);

            /*
            Task<string> s = api.GetWeather("Rankweil");
            string ss = await s;
            Debug.WriteLine(ss);
        } */
    }


}
