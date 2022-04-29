using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace weather_widget.Model
{
    class APIManagerModel
    {
        private static string API_KEY = "809a3aafdef755e5e0e431bd7bc48824";

        // TO DO: Do this in DataBase Manager
        #region private constants for units
        private const string unitWinSpeed = "m/s";
        private const string unitIcon = ".png";
        private const string unitTemp = "°C";
        private const string unitHumidity = "%";
        #endregion

        //        public async Task<List<WeatherDailyInfoModel>> GetWeather(string location)
        // public async Task<string> GetWeather(string location)

        public async Task<WeatherInfoListModel> GetWeather(string location)
        {
            string url = $"https://api.openweathermap.org/data/2.5/forecast?q={location}&mode=json&units=metric&appid={API_KEY}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(url);
                    // No error occured (internet connection available & max request is not exceeded)
                    if(!(response.Contains(@"""message"": ""Your account is temporary""") && response.Contains(@"""cod"": 429")))
                    {
                        return GetWeatherInfos(response.ToString());
                    }
                    else
                    {
                        // TO DO: give info for searching in sqlite db
                        // for now: just return an empty list of weatherinfos
                        return new WeatherInfoListModel();
                    }
                }
                catch (HttpRequestException ex)
                {
                    /*
                    // TO DO: do something, if it goes wrong
                    // for now: just return an empty list of weahterinfos
                    return new List<WeatherInfoModel>(); 
                    */
                    return new WeatherInfoListModel();
                }
            }
        }

        private static WeatherInfoListModel GetWeatherInfos(string JSONContent)
        {
            JSONResponce jSONResponce = JsonConvert.DeserializeObject<JSONResponce>(JSONContent);
            Debug.WriteLine(JsonConvert.DeserializeObject<JSONResponce>(JSONContent).Items[0].WeatherWindInfo.WindDirection);

            // TO DO: Convert it into WeatherInfoModel --> Done

            // Weather states (each item 3 hours apart)
            WeatherInfoListModel weatherInfos = new WeatherInfoListModel();

            // Add WeatherInfoModel to WeatherInfoListModel
            foreach (var item in jSONResponce.Items)
            {
                weatherInfos.Add(ToWeatherInfoModel(item));
            }
            return weatherInfos;
        }

        /// <summary>
        /// Convert ListItem object to WeatherInfoModel object
        /// </summary>
        /// <param name="item">Weather data (for every 3 hours)</param>
        /// <returns></returns>
        private static WeatherInfoModel ToWeatherInfoModel(JSONListItem item)
        {
            return new WeatherInfoModel
            (
                weatherdesc: item.WeatherTypes[0].Description,
                weathericon: item.WeatherTypes[0].Icon,
                weatherdaytime: DateTime.Parse(item.DateTime), // content: string DateTime --> Parse to DateTime
                maxtemp: double.Parse(item.MainInfo.Temp_max.ToString()),
                mintemp: double.Parse(item.MainInfo.Temp_min.ToString()),
                winddir: double.Parse(item.WeatherWindInfo.WindDirection.ToString()),
                windspeed: double.Parse(item.WeatherWindInfo.WindSpeed.ToString()),
                humidity: double.Parse(item.MainInfo.Humidity.ToString())
            );
        }

        /*
        TO DO: Do this in Database manager --> User will access to data only with Databasemanager NOT APIManager!!!
        /// <summary>
        /// Converts the received value into a direction, which is understandable (as String)
        /// </summary>
        /// <param name="winddir"></param>
        /// <returns>String dir</returns>
        private static string WindDirConverter(string winddir)
        {
            double degree = double.Parse(winddir);

            int fixeddegree = int((degree / 22.5) + .5);
            arr[] = ["N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW"];
            return arr[(val % 16)];
        }
        */
    }
}
