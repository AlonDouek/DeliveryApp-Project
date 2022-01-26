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
    class SignUpPageViewModel
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
                this.ShowEmailError = Email.Contains("@") && Email.EndsWith(".com");
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
            DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();

            if (ValidateForm())
            {
                User user = await proxy.SignUpAsync(Email, Username, Password, Address, PhoneNumber, CreditCard);

                App theApp = (App)App.Current;
                theApp.CurrentUser = user;

                //Page p = new NavigationPage(new Views.UserPage());
                //App.Current.MainPage = p;
                Push?.Invoke(new Views.UserPage());
            }
        }

        public event Action<Page> Push;
    }
}