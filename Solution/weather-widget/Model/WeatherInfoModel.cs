using System;
using System.Collections.Generic;
using System.Text;

namespace weather_widget.Model
{
    class WeatherInfoModel
    {
        #region properties

        // Description of current weather
        public string Weather { get; set; }
        // Weathericon path --> Weathericon; for displaying the icon e.g. xxxFOLDERxxx/icons/04d.png
        public string WeatherIcon { get; set; }
        // Weatherday as day of week
        public string WeatherDay { get; set; }
        // Temperature in Celsius
        public string Temperature { get; set; }
        // Max. Temperature in Celsius
        public string MaxTemperature { get; set; }
        // Min. Temperature in Celsius
        public string MinTemperature { get; set; }
        // Wind direction as text
        public string WindDirection { get; set; }
        // Wind speed in m/s
        public string WindSpeed { get; set; }
        // Humidity in %
        public string Humidity { get; set; }
        #endregion
    }
}
