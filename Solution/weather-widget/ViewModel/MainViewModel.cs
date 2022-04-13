using weather_widget.Store;

namespace weather_widget.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }



        public ViewModelBase CurrentViewModel { get => _navigationStore.CurrentViewModel; }


    }
}
