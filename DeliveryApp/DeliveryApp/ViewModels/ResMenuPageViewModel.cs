using DeliveryApp.Services;
using DeliveryServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace DeliveryApp.ViewModels
{
    class ResMenuPageViewModel
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        //private string email;
        //public string Email
        //{
        //    get { return email; }
        //    set
        //    {
        //        email = value;
        //        OnPropertyChanged("Email");
        //    }
        //}


        public ObservableCollection<Restaurant> ResList { get; }

        public ResMenuPageViewModel()
        {
            ResList = new ObservableCollection<Restaurant>();
            
            CreateResCollection();
        }
        async void CreateResCollection()
        {
            DeliveryAPIProxy proxy = new DeliveryAPIProxy();
            List<Restaurant> theRestaurants = await proxy./*[Add later]*/();
            foreach (Restaurant m in theRestaurants)
            {
                this.ResList.Add(m);
            }
        }

    }
}
