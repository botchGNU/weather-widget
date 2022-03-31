using System;
using System.Collections.Generic;
using System.Text;
using weather_widget.Store;
using weather_widget.ViewModel;

namespace weather_widget.Command
{
    internal class NavigateCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;

        public NavigateCommand(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            this._navigationStore = navigationStore;
            this._createViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
