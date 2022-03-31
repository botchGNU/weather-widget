using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            //_navStore.CurrentViewModel = new XXXX();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }




    
}
