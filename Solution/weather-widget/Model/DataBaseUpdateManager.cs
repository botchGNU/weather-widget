using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace weather_widget.Model
{
    public class DataBaseUpdateManager
    {
        private string _currentCity;
        private DataBaseManagerModel _manager;
        private Timer _threeHourTimer;

        #region ctor
        public DataBaseUpdateManager()
        {
            _manager = new DataBaseManagerModel();
            CurrentCity = "Rankweil";   //current default value
            UpdateWeather();      //uncommented unless api key is in repo
            SetTimer();
        }
        #endregion

        #region methods
        // call the update method from the Database Manager + pass current city
        public void UpdateWeather()
        {
            if (IsConnectionAvailable())
            {
                _manager.GetDataFromOpenWeather(CurrentCity);
            }
            else
            {
                Debug.WriteLine("No connection available");
            }
        }
        // set 3h timer for updating weatherlist
        private void SetTimer()
        {
            
            _threeHourTimer = new System.Timers.Timer(10800000);    // Create a timer with a 3h interval.
            _threeHourTimer.Elapsed += OnTimedEvent;    // Hook up the Elapsed event for the timer. 
            _threeHourTimer.AutoReset = true;
            _threeHourTimer.Enabled = true;
        }
        
        //check if internet connection is available
        private bool IsConnectionAvailable()
        {
            try
            {   //check if domain name is resolvable
                System.Net.IPHostEntry ipHe =  System.Net.Dns.GetHostEntry("www.openweathermap.org");
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region events
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            UpdateWeather();
        }
        #endregion

        #region properties
        public string CurrentCity { get => _currentCity; set => _currentCity = value; }
        #endregion 
    }
}
