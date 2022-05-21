using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace weather_widget.Model
{
    /// <summary>
    /// Weather data for each day, 3h forecasts
    /// </summary>
    public class WeatherInfoListModel : List<WeatherInfoModel>
    {
        /// <summary>
        /// WeatherInfoListModel constructor
        /// </summary>
        public WeatherInfoListModel()
        {

        }        
    }

}
