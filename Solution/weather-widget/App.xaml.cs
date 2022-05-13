using System.Windows;
using weather_widget.Model;
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
        private DataBaseUpdateManager _updateManager;
        public App()
        {
            _navStore = new NavigationStore();
            _updateManager = new DataBaseUpdateManager();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navStore.CurrentViewModel = new DashboardViewModel(_navStore, CreateSettingsViewModel, _updateManager);    //Dashboard -> startup window

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }


        #region create methods
        /// <summary>
        /// Methods in order to create new ViewModeles -> Passed into ViewModels
        /// </summary>
        /// <returns></returns>
        private DashboardViewModel CreateDashboardViewModel()
        {
            return new DashboardViewModel(_navStore, CreateSettingsViewModel, _updateManager);
        }

        private SettingsViewModel CreateSettingsViewModel()
        {
            return new SettingsViewModel(_navStore, CreateDashboardViewModel, _updateManager);
        }
        #endregion
    }
}
