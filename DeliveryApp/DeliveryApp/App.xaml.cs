using DeliveryApp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DeliveryApp.Views;

namespace DeliveryApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv
        {
            get
            {
                return true; //change this before release!
            }
        }
        //The current logged in user
        public User CurrentUser { get; set; }
        //The current user's order
        public Order CurrentOrder { get; set; }

        public App()
        {
            InitializeComponent();
            CurrentUser = null;
            CurrentOrder = null;
            //MainPage = new NavigationPage(new UserPage());//MainPage();

            MainPage = new NavigationPage(new LogInPage());//MainPage();
        }
        public void NullCurrentUser()
        {
            CurrentUser = null;
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
