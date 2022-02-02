using DeliveryApp.Models;
using DeliveryApp.Services;
using DeliveryApp.Views;
using DeliveryServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DeliveryApp.ViewModels
{
    class SignUpPageViewModel
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region props
        #region Email
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
        #endregion

        #region password
        private string password;

        public string Password
        {
            get { return password; }

            set
            {
                password = value;
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

        #endregion

        #region username
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }
        #endregion

        #region address
        private string address;

        public string Address
        {
            get { return address; }

            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        #endregion

        #region phoneNumber
        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }

            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        #endregion

        #region Credit Card
        private string creditCard;
        public string CreditCard
        {
            get { return creditCard; }

            set
            {
                creditCard = value;
                OnPropertyChanged("CreditCard");
            }
        }
        #endregion
        #endregion


        public SignUpPageViewModel()
        {
          
            PasswordError = "must type password";
            ShowPasswordError = false;
            EmailError = "must type correct Email";
            ShowEmailError = false;
            this.SignUpCommand = new Command(() => OnRegister());
        }
        //fix validation
        private void ValidatePassword()
        {
            if (!string.IsNullOrEmpty(Password))
                this.ShowPasswordError = Password.Length > 5 && Password.Length < 30;
            else
                this.showPasswordError = true;
        }
        private void ValidateEmail()
        {
            if (!string.IsNullOrEmpty(Email))
                this.ShowEmailError = !(Email.Contains("@") && Email.EndsWith(".com"));
            else
                this.ShowEmailError = true;
        }

        private bool ValidateForm()
        {
            ValidateEmail();
            ValidatePassword();

            return !(ShowEmailError || ShowPasswordError);
        }
 
        public Command SignUpCommand { protected set; get; }

        public async void OnRegister()
        {

            if (ValidateForm())
            {
                DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();
                User u = new User
                {
                    Email = Email,
                    Password = Password,
                    Username = Username,
                    Address = Address,
                    CreditCard = CreditCard,
                    PhoneNumber = PhoneNumber
                };

                User user = await proxy.SignUpAsync(u);

                if(user != null)
                {
                    App theApp = (App)App.Current;
                    theApp.CurrentUser = user;

                    App.Current.MainPage = new UserPage();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "sign up failed, please check and try again", "OK");
                }
            }
        }

    }
}