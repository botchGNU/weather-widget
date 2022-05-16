using System;
using System.Collections.Generic;
using System.Text;
using weather_widget.Model;
using weather_widget.ViewModel;

namespace weather_widget.Command
{
    internal class UpdateCityCommand : CommandBase
    {
        private DataBaseUpdateManager _updateMan;
        public UpdateCityCommand(DataBaseUpdateManager updateManager){_updateMan = updateManager; }


        public override void Execute(object parameter)
        {
            _updateMan.UpdateWeather();
        }
    }
}
