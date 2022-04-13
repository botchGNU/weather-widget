using System.Windows;
using weather_widget.Store;
using weather_widget.ViewModel;

namespace weather_widget
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {
        private readonly NavigationStore _navStore; 

        public App()
        {
            _navStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navStore.CurrentViewModel = new DashboardViewModel(_navStore, CreateSettingsViewModel);    //Dashboard -> startup window

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }


        #region create methods
        private DashboardViewModel CreateDashboardViewModel()
        {
            return new DashboardViewModel(_navStore, CreateSettingsViewModel);
        }

        private SettingsViewModel CreateSettingsViewModel()
        {
            return new SettingsViewModel(_navStore, CreateDashboardViewModel);
        }
        #endregion
    }



}
