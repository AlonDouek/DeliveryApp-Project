using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DeliveryApp.Models;
using System.Collections.ObjectModel;

namespace DeliveryApp.ViewModels
{
    class showResViewModel
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        #region props

        #region Name

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        #endregion

        #region Description

        private string description;

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        #endregion

        #region opening hours

        private TimeSpan openingHours;

        public TimeSpan OpeningHours
        {
            get => openingHours;
            set
            {
                openingHours = value;
                OnPropertyChanged("OpeningHours");
            }
        }

        #endregion

        #region closing hours
        private TimeSpan closingHours;

        public TimeSpan ClosingHours
        {
            get => closingHours;
            set
            {
                closingHours = value;
                OnPropertyChanged("ClosingHours");
            }
        }

        #endregion


        #endregion
            
        public ObservableCollection<Menu> menuList { get; }    
            
        public showResViewModel()
        {
            
        }

    }
}
