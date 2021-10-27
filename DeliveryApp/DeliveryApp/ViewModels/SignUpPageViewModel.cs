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
    class SignUpPageViewModel
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region FILL ERROR
        //ADD THE THINGS THAT NEED TO BE ADDED
        private string fillError;
        public string FillError
        {
            get { return fillError; }
            set
            {
                fillError = value;
                OnPropertyChanged("FillError");
            }
        }
        private bool showFillError;

        public bool ShowFillError
        {
            get => showFillError;
            set
            {
                showFillError = value;
                OnPropertyChanged("ShowFillError");
            }
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


        //still not 100% sure if i do this might remove later think about it

        //Maybe slpilt

        //private PaymentInfo paFo;
        //public PaymentInfo PaFo
        //{
        //    get { return paFo; }
        //    set
        //    {
        //        paFo = value;
        //        OnPropertyChanged("PaFo");
        //    }
        //}
        
        public ICommand SubmitCommand { protected set; get; }

        public SignUpPageViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }
        public async void OnSubmit()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.UserPage());
            DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();
            User user = await proxy.LoginAsync(Email, Password);
            if (user == null)
            {

                await App.Current.MainPage.DisplayAlert("error", "Log In failed please try again", "ok");
            }
            else
            {
                App theApp = (App)App.Current;
                theApp.CurrentUser = user;

                Page p = new NavigationPage(new Views.UserPage());
                App.Current.MainPage = p;

                //d
            }
        }
    }
}
//https://www.pinterest.com/pin/663366220106226478/ MAYBE HELP IN FUTURE