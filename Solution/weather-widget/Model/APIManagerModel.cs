using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace weather_widget.Model
{
    class APIManagerModel
    {
        private static string API_KEY = File.ReadAllText(@"..\\..\\..\\..\\..\\API.key"); //=> ".\weather-widget\API.key"

        // TO DO: Do this in DataBase Manager
        #region private constants for units
        private const string unitWinSpeed = "m/s";
        private const string unitIcon = ".png";
        private const string unitTemp = "°C";
        private const string unitHumidity = "%";
        #endregion

        public async Task<WeatherInfoListModel> GetWeather(string location)
        {
            string url = $"https://api.openweathermap.org/data/2.5/forecast?q={location}&mode=json&units=metric&appid={API_KEY}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(url);
                    // No error occured (internet connection available & max request is not exceeded)
                    if(!(response.ToUpper().Contains(@"""MESSAGE"": ""YOUR ACCOUNT IS TEMPORARY""") && response.Contains(@"""COD"": 429")))
                    {
                        return GetWeatherInfos(response.ToString());
                    }
                    else if(response.ToUpper().Contains(@"""MESSAGE"": ""YOUR ACCOUNT IS TEMPORARY""") || response.Contains(@"""COD"": 429"))
                    {
                        throw new Exception("1: Max request reached!"); // Maximum reached
                    }
                    else if(response.ToUpper().Contains(@"""MESSAGE"": ""INVALID API KEY""") || response.Contains(@"""COD"": 401"))
                    {
                        throw new Exception("1: Invalid API-Key!"); // API-Key is wrong
                    }
                    else if(response.ToUpper().Contains(@"""MESSAGE"": ""INVALID API KEY""") || response.Contains(@"""COD"": 401"))
                    {
                        // TO DO: give info, that max req. is reached --> search in sqlite db
                        throw new Exception("1: Max request is reached!");
                    }
                    else if (response.ToUpper().Contains(@"""MESSAGE"": ""INVALID API KEY""") || response.Contains(@"""COD"": 401"))
                    {
                        // TO DO: give info, that max req. is reached --> search in sqlite db
                        throw new Exception("1: Max request is reached!");
                    }
                }
                catch (HttpRequestException)
                {
                    // TO DO: No internet connection or API-Key is invalid
                    throw new Exception("2: No internet connection or API-Key is invalid!");
                }
                catch (Exception)
                {
                    // TO DO: Something went wrong
                    throw new Exception("3: Something went worng!");
                }
                return null;
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
                winddirasstring: WindDirConverter(double.Parse(item.WeatherWindInfo.WindDirection.ToString())),
                windspeed: double.Parse(item.WeatherWindInfo.WindSpeed.ToString()),
                humidity: double.Parse(item.MainInfo.Humidity.ToString())
            );
        } 
        //TO DO: Do this in Database manager --> User will access to data only with Databasemanager NOT APIManager!!!7


        /// <summary>
        /// Converts the received value into a direction, which is understandable (as String)
        /// </summary>
        /// <param name="winddir"></param>
        /// <returns>String dir</returns>
        private static string WindDirConverter(double winddir)
        {
            double degree = winddir;

            int fixeddegree = (int)((degree / 22.5) + .5);
            string[] arr = new string[] { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };
            return arr[(fixeddegree % 16)];
        }
    }
}
