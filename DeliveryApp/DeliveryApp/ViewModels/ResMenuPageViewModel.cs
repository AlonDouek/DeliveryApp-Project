using DeliveryApp.Models;
using DeliveryApp.Services;
using DeliveryApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Menu = DeliveryApp.Models.Menu;

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

        private Menu meno;
        public Menu Meno
        {
            get => meno;
            set
            {
                meno = value;
                OnPropertyChanged("Meno");
            }
        }

        private ObservableCollection<Restaurant> resList;
        public ObservableCollection<Restaurant> ResList
        {
            get => resList;
            set
            {
                resList = value;
                OnPropertyChanged("ResList");
            }
        }

        public ResMenuPageViewModel()
        {
            ResList = new ObservableCollection<Restaurant>();
            CreateResCollection();
            
        }
        async void CreateResCollection()
        {
            try
            {
                DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();
                List<Restaurant> theRestaurants = await proxy.GetAllRestaurantsAsync();
                foreach (Restaurant m in theRestaurants)
                {
                    this.ResList.Add(m);
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
        }
        public async void CreateMenu(Restaurant m)
        {
            try
            {
                DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();
                Meno = await proxy.GetMenuAsync(m.RestaurantId);
                
               // Menu = await proxy.GetMenuAsync(Name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        //fix =>
        public ICommand SelctionChanged => new Command<Object>(OnSelectionChanged);
        public void OnSelectionChanged(Object obj)
        {
            if (obj is Restaurant)
            {
                Restaurant choice = (Restaurant)obj;
                this.CreateMenu(choice);
                Page resPage = new ShowRes();
               
                showResViewModel Context = new showResViewModel
                {
                    Id = choice.RestaurantId,
                    Name = choice.Name,
                    Description = choice.Description,
                    OpeningHours = choice.OpeningHours,
                    ClosingHours = choice.ClosingHours,
                    Menu1 = Meno
                };
                Context.addMenuItem();
                resPage.BindingContext = Context;
                resPage.Title = Context.Name;
                
                App.Current.MainPage = resPage;
            }
        }



    }
}
