using DeliveryApp.Models;
using DeliveryApp.Services;
using DeliveryApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DeliveryApp.ViewModels
{
    class ChangeCredentialsPageViewModel
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region props
        private User CurrentUser;

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

        #endregion

        #region Error
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
                OnPropertyChanged(Error);
            }
        }

        #endregion

        private void ValidatePassword()
        {
            int breakP = 0;
            if(string.IsNullOrEmpty(Password))
            {
                this.passwordError = "";
                this.showPasswordError = false;
            }
            else if (Password.Length < 5 || Password.Length > 30)
            {
                this.passwordError = "must type password between 5-30 character! ";
                this.ShowPasswordError = true;
            }
            else
            {
                this.passwordError = "";
                this.showPasswordError = false;
            }
            
            
        }
        private void ValidateEmail()
        {
            int breakP = 0;
            if ((string.IsNullOrEmpty(Email)))
            {
                this.EmailError = "";
                this.ShowEmailError = false;
            }
            else if ((!(Email.Contains("@") && Email.EndsWith(".com"))))
            {
                this.EmailError = "Email must be typed Correctly ";
                this.ShowEmailError = true;
            }
            else
            {
                this.EmailError = "";
                this.ShowEmailError = false;
            }
        }
        private bool ValidateForm()
        {
            this.Error = "";
            int breakP = 0;
            if (((App)App.Current).CurrentUser == null)
            {

                return false;
            }

            ValidateEmail();
            ValidatePassword();

            this.Error += this.EmailError + this.passwordError + ", please check and try again";
            return !((ShowEmailError && ShowPasswordError) || (ShowEmailError || ShowPasswordError));

        }

        public ICommand ChangeCredentialCommand { protected set; get; }

        public ChangeCredentialsPageViewModel()
        {
            CurrentUser = ((App)App.Current).CurrentUser;
            Error = "";
            PasswordError = "must type password";
            ShowPasswordError = false;
            EmailError = "must type correct Email";
            ShowEmailError = false;
            this.ChangeCredentialCommand = new Command(() => ChangeCredential());
        }
        public async void ChangeCredential()
        {
            DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();
            try
            {
                bool check = ValidateForm();
                if (!check)
                {
                    await App.Current.MainPage.DisplayAlert("Error", Error, "OK");
                }
                else
                {
                    App theApp = (App)App.Current;
                    User Current = theApp.CurrentUser;

                    bool signUp = await proxy.ChangeCredentialsAsync(Current.Email,Email,Password,Username,Address,CreditCard,PhoneNumber);
                   
                    if (Username != null)
                        Current.Username = Username;
                    if (Address != null)
                        Current.Address = Address;
                    if (PhoneNumber != null)
                        Current.PhoneNumber = PhoneNumber;
                    if (CreditCard != null)
                        Current.CreditCard = CreditCard;
                    if (Password != null)
                        Current.Password = Password;
                    if (email != null)
                        Current.Email = Email;

                    Current = theApp.CurrentUser;
                    if (signUp)
                        App.Current.MainPage = new TEMPVIEW1();
                    else
                        await App.Current.MainPage.DisplayAlert("Error", "everything went completly wrong!, please try again", "OK");

                    
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }




        
    }
}
