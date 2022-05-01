using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace weather_widget.Model
{
    public class DataBaseManagerModel
    {
        private WeatherInfoListModel weatherInfos;
        private string FilePath = @"..\\..\\..\\..\\..\\WeatherInfo.db";
        private bool receivedJSON = false;

        public void GetDataFromOpenWeather(string cityname)
        {
            try
            {
                GetWeather(cityname);
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
        }
        private void SaveIntoDatabase(string CityName, int CityId, string CountryZip)
        {
            if (weatherInfos != null)
            {
                // TODO: Check if Cityname exists in Database Citylist!!
                weatherInfos.cityid = CityId;
                weatherInfos.countryzip = CountryZip;
                weatherInfos.cityname = CityName;

                weatherInfos.SaveToSqlite(FilePath);
            }
        }
        private async void GetWeather(string cityname)
        {
            APIManagerModel apimanagerModel = new APIManagerModel();
            Task<WeatherInfoListModel> TaskweatherInfos = apimanagerModel.GetWeather(cityname);

            weatherInfos = await TaskweatherInfos;
            Debug.WriteLine(weatherInfos.Count);
            weatherInfos.cityid = 0;
            weatherInfos.countryzip = "AT";
            SaveIntoDatabase(cityname, weatherInfos.cityid, weatherInfos.countryzip);
        }
    }
}
