using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DeliveryApp.ViewModels
{
    class ItemCartPageViewModel
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
