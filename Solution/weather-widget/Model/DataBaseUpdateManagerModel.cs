using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace weather_widget.Model
{
    public class DataBaseUpdateManagerModel
    {
        #region fields
        private string _currentCity;
        private DataBaseManagerModel _manager = new DataBaseManagerModel();
        private Timer _threeHourTimer;
        #endregion

        #region ctor
        public DataBaseUpdateManagerModel()
        {
            CurrentCity = "Rankweil";   //current default value
            UpdateWeather();      //uncommented unless api key is in repo
            SetTimer();
        }
        #endregion

        #region methods
        // call the update method from the Database Manager + pass current city
        public void UpdateWeather()
        {
            _manager.GetDataFromOpenWeather(CurrentCity);
        }
        // set 3h timer for updating weatherlist
        private void SetTimer()
        {
            
            _threeHourTimer = new System.Timers.Timer(10800000);    // Create a timer with a 3h interval.
            _threeHourTimer.Elapsed += OnTimedEvent;    // Hook up the Elapsed event for the timer. 
            _threeHourTimer.AutoReset = true;
            _threeHourTimer.Enabled = true;
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
