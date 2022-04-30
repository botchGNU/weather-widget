using System;
using System.Collections.Generic;
using System.Linq;

namespace weather_widget.Model
{
    /// <summary>
    /// Weather data for each day, 3h forecasts
    /// </summary>
    class WeatherInfoListModel : List<WeatherInfoModel>
    {
        /// <summary>
        /// WeatherInfoListModel constructor
        /// </summary>
        public WeatherInfoListModel()
        {}

        /*
        /// <summary>
        /// Get the average temperature for the day
        /// </summary>
        /// <returns>Average temperature for the day</returns>
        public double GetAVGTemp()
        {
            return (GetMinTemperature() + GetMaxTemperature()) / 2;
        }

        /// <summary>
        /// Get the minimum temperature for the day
        /// </summary>
        /// <returns>Minimum temperature for the day</returns>
        public double GetMinTemperature(bool round = true)
        {
            double result = double.Parse(this[0].MinTemperature.ToString());

            foreach (WeatherInfoModel weather in this)
            {
                if (result > weather.MinTemperature)
                {
                    result = weather.MinTemperature;
                }
            }

            return round ? Math.Round(result) : result;
        }

        /// <summary>
        /// Get the maximum temperature for the day
        /// </summary>
        /// <returns>Minimum temperature for the day</returns>
        public double GetMaxTemperature(bool round = true)
        {
            double result = double.Parse(this[0].MinTemperature.ToString());

            foreach (WeatherInfoModel weather in this)
            {
                if (result < weather.MinTemperature)
                {
                    result = weather.MinTemperature;
                }
            }

            return round ? Math.Round(result) : result;
        }

        /// <summary>
        /// Get the date
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetDate()
        {
            return new DateTime(this[0].WeatherDayTime.Year, this[0].WeatherDayTime.Month, this[0].WeatherDayTime.Day);
        }


        /// <summary>
        /// Gives you the highest or lowest windspeed for the day
        /// </summary>
        /// <param name="highest">True for highest windspeed of that day</param>
        /// <returns>double WindSpeed (default: highest = true) in m/s</returns>
        public double GetWindSpeed(bool highest = true)
        {
            double result = double.Parse(this[0].WindSpeed.ToString());

            foreach (WeatherInfoModel weather in this)
            {
                if(highest)
                {
                    if (result < weather.WindSpeed)
                    {
                        result = weather.WindSpeed;
                    }
                }
                else
                {

                    if (result > weather.WindSpeed)
                    {
                        result = weather.WindSpeed;
                    }
                }
                
            }

            return result;
        }

        // TO DO:
        /*
        public string GetWindDirection()
        {
            
        }
        */

        /*
        /// <summary>
        /// Get the frequently recurring weather type for the day
        /// </summary>
        /// <returns></returns>
        public string GetFrequentDescription()
        {
            var uniqueWeatherStatess = EveryDayWeatherStates.OrderByDescending(i => i.WeatherDescription).Distinct(j => j.WeatherDescription).ToList();

            if (uniqueWeatherStatess.Count > 0)
            {
                return $"{uniqueWeatherStatess[0].Weather}";
            }
            else return "no weather data";

        }
        */
    }

}
