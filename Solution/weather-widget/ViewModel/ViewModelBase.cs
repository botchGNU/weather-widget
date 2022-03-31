using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace weather_widget.ViewModel
{
    /// <summary>
    /// Basis, von welcher alle ViewModels erben -> Einfacher INotifyPropertyChanged einzubinden
    /// </summary>

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
