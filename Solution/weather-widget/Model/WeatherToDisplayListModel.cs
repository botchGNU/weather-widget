using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace weather_widget.Model
{
    public class WeatherToDisplayListModel : ObservableCollection<WeatherToDisplay>
    {
        public void FillTest()
        {
            for (int i = 0; i < 5; i++)
            {
                var weatherNew = new WeatherToDisplay("Cloudy", "02n.png", Convert.ToString(i + 10), Convert.ToString(i), Convert.ToString(22), Convert.ToString(i + 5), Convert.ToString(50));
                this.Add(weatherNew);
            }
        }
    }
}
