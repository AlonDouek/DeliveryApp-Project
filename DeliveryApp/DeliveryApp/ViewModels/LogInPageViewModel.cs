using DeliveryApp.Models;
using DeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DeliveryApp.ViewModels
{
    class LogInPageViewModel
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }

            set
            {
                password = value;
                ValidatePassword(); 
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private void ValidatePassword()
        {
            if(!string.IsNullOrEmpty(Password))
                this.ShowPasswordError = Password.Length > 5 && Password.Length < 30;
        }
       
        public ICommand SubmitCommand { protected set; get; }

        public LogInPageViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
            PasswordError = "password must be between 5-30 character!";
            showPasswordError = false;
            
        }

        public async void OnSubmit()
        {
            if (Email != "" && Password != "")
            {
                ValidatePassword();
                if (ShowPasswordError)
                {
                    await App.Current.MainPage.DisplayAlert("error", "Password Must be between 5-30 characters", "ok");
                }
                else
                {
                    DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();
                    User user = await proxy.LoginAsync(Email, Password);
                    if (user != null)
                    {
                        App theApp = (App)App.Current;
                        theApp.CurrentUser = user;

                        Page p = new NavigationPage(new Views.UserPage());
                        App.Current.MainPage = p;

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("error", "User not Logged In", "ok");
                    }
                }
                
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("error", "Log In failed! you must fill email and password", "ok");

            }
        }

    }
}
