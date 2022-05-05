using System;
using System.Collections.Generic;
using System.Text;
using weather_widget.ViewModel;

namespace weather_widget.Command
{
    internal class UpdateCityCommand : CommandBase
    {
        private SettingsViewModel _viewModel;
        public UpdateCityCommand(SettingsViewModel viewModel){ _viewModel = viewModel; }

        public override void Execute(object parameter)
        {
            //not implemented yet
        }
    }
}
