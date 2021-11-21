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
    class UserPageViewModel
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
                OnPropertyChanged("Password");
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
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

        public ICommand ChangeCredentialCommand { protected set; get; }

        public UserPageViewModel()
        {
            ChangeCredentialCommand = new Command(MoveChangeCredential);
        }
        public async void MoveChangeCredential()
        {
            DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();
            App theApp = (App)App.Current;
            User u = new User(theApp.CurrentUser);
            if ( u != null)
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ChangeCredentialsPage());
                Page p = new NavigationPage(new Views.ChangeCredentialsPage());
                App.Current.MainPage = p;

                //d
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("error", "failed, please Log In", "ok");
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.LogInPage());

            }
        }
    }
}
