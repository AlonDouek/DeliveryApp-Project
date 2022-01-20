using DeliveryApp.Models;
using DeliveryApp.Services;
using DeliveryServer.Models;
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
        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
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

        private string error;

        public string Error
        {
            get => error;
            set
            {
                error = value;
                OnPropertyChanged("Error");
            }
        }

        private bool showError;

        public bool ShowError
        {
            get => showError;
            set
            {
                showError = value;
                OnPropertyChanged("ShowError");
            }
        }


        private void ValidatePassword()
        {
            if(!string.IsNullOrEmpty(Password))
                this.ShowPasswordError = Password.Length > 5 && Password.Length < 30;
            else
                this.showPasswordError = true;
        }
        private void ValidateEmail()
        {
            if (!string.IsNullOrEmpty(Email))
            {
                try
                {
                    var VE = new System.Net.Mail.MailAddress(Email);
                    this.ShowEmailError = VE.Address != Email;
                }
                catch
                {
                    this.ShowEmailError = true;
                }
            }
            else
                this.ShowEmailError = true;
           

        }

        public ICommand SubmitCommand { protected set; get; }

        public LogInPageViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
            PasswordError = "must type password between 5-30 character!";
            ShowPasswordError = false;
            EmailError = "must type correct Email";
            ShowEmailError = false;
        }

        private bool ValidateForm()
        {
            if (((App)App.Current).CurrentUser != null)
            {
                Error = "You are already logged in";
                return false;
            }

            ValidateEmail();
            ValidatePassword();

            return !(ShowEmailError || ShowPasswordError);
        }
        public async void OnSubmit()
        {
            if (Email != "" && Password != "")
            {
                ValidateForm();
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


