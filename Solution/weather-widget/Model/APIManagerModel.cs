using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace weather_widget.Model
{
    static class APIManagerModel
    {
        private static string API_KEY = "xxxxxPleaseCopyYourKEYxxxx";

        public static async Task<List<WeatherInfoModel>> GetWeather(string location)
        {
            string url = $"https://api.openweathermap.org/data/2.5/forecast?q={location}&mode=json&units=metric&appid={API_KEY}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(url);
                    // No error occured (internet connection available
                    if(!response.Contains("message") && response.Contains("cod"))
                    {
                        return GetWeatherInfos("Input, extract it and return a list of weather forecasts");
                    }
                    else
                    {
                        // TO DO: give info for searching in sqlite db
                        // for now: just return an empty list of weatherinfos
                        return new List<WeatherInfoModel>();
                    }
                }
                catch (HttpRequestException ex)
                {
                    // TO DO: do something, if it goes wrong
                    // for now: just return an empty list of weahterinfos
                    return new List<WeatherInfoModel>(); 
                }
            }
        }

        private static List<WeatherInfoModel> GetWeatherInfos(string JSONContent)
        {

            return new List<WeatherInfoModel>();
        }

        //private static string DayOfTheWeek()
    }
}
