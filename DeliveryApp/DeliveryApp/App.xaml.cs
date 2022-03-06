﻿using DeliveryApp.Models;
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

        public App()
        {
            InitializeComponent();
            CurrentUser = null;
            //MainPage = new NavigationPage(new SignUpPage());//MainPage();

            MainPage = new NavigationPage(new LogInPage());//MainPage();
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
