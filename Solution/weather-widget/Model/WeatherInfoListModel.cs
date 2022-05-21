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
        #region properties for saving to DB
        public string cityname { get; set; } = string.Empty;
        public int cityid { get; set; } = 0;
        public string countryzip { get; set; } = string.Empty;
        public int id { get; set; } = -1;
        #endregion

        /// <summary>
        /// WeatherInfoListModel constructor
        /// </summary>
        public WeatherInfoListModel()
        {

        }        
    }

}
