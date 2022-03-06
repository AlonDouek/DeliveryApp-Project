﻿using DeliveryApp.Models;
using DeliveryApp.Services;
using DeliveryApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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
            DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();
            List<Restaurant> theRestaurants = await proxy.GetAllRestaurantsAsync();
            foreach (Restaurant m in theRestaurants)
            {
                this.ResList.Add(m);
            }
        }

        ////fix =>
        //public ICommand SelctionChanged => new Command<Object>(OnSelectionChanged);
        //public void OnSelectionChanged(Object obj)
        //{
        //    if (obj is Restaurant)
        //    {
        //        Restaurant choice = (Restaurant)obj;
        //        Page resPage = new ShowRes();
        //        ShowResViewModel Context = new ShowResViewModel
        //        {
        //            Name = choice.Name,
        //            Description = choice.Description,
        //            OpeningHours  = choice.OpeningHours,
        //            ClosingHours = choice.ClosingHours
        //        };
        //        //resPage.BindingContext = Context;
        //        //resPage.Title = Context.Name;
        //        //if (NavigateToPageEvent != null)
        //        //    NavigateToPageEvent(resPage);
        //    }
        //}



        #region Events
        //Events
        //This event is used to navigate to the monkey page
        public Action<Page> NavigateToPageEvent;
        #endregion

    }
}
