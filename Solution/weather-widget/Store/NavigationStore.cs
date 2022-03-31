using System;
using System.Collections.Generic;
using System.Text;
using weather_widget.ViewModel;

namespace weather_widget.Store
{
    /// <summary>
    /// Speichert und navigiert derzeitiges ViewModel
    /// </summary>
    public class NavigationStore
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }


    }
}
