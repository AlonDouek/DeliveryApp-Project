using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DeliveryApp.Models;
using System.Collections.ObjectModel;
using DeliveryApp.Services;

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

        #region ID

        private int id;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        #endregion

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

        private Menu menu1;

        public Menu Menu1
        {
            get => menu1;
            set
            {
                menu1 = value;
                OnPropertyChanged("Menu1");
            }
        }


        #endregion


        private ObservableCollection<MenuItem> menuItemList;
        public ObservableCollection<MenuItem> MenuItemList
        {
            get => menuItemList;
            set
            {
                menuItemList = value;
                OnPropertyChanged("MenuItemList");
            }
        }

        private Menu menuList;
        public Menu MenuList
        {
            get => menuList;
            set
            {
                menuList = value;
                OnPropertyChanged("MenuList");
            }
        }

        public showResViewModel()
        {
            MenuList = new Menu();
            //CreateMenu();
            MenuItemList = new ObservableCollection<MenuItem>();
            //addMenuItem();
        }
        public async void CreateMenu()
        {

            try
            {
                DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();
                MenuList = await proxy.GetMenuAsync(Id);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public async void addMenuItem()
        {
            try
            {
                DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();
                List<MenuItem> theMenuItems = await proxy.GetItemsByMenuIDAsync(MenuList.MenuId);
                foreach (MenuItem m in theMenuItems)
                {
                    this.MenuItemList.Add(m);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
