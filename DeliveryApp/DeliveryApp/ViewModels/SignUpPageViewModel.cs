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

        }

        public ICommand Register => new Command(OnRegister);

        
        public async void OnRegister()
        {
            
                //TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                //User u = new User()
                //{
                //    Email = Email,
                //    NickName = Username,
                //    Password = Password,
                //    Questions = new List<AmericanQuestion>()
                //};
                //bool valid = await proxy.RegisterUser(u);
                //if (valid)
                //{
                //    User user = await proxy.LoginAsync(Email, Password);
                //    App theApp = (App)App.Current;
                //    theApp.CurrentUser = user;
                //    ((App)App.Current).currentUser = user;
                //    PopToRoot.Invoke();
                //}
                //else
                //    ErrorMessage = "Something went wrong! Please try again later.";
            



            await App.Current.MainPage.Navigation.PushModalAsync(new Views.UserPage());
            DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();
            User user = await proxy.LoginAsync(Email, Password);
            if (user == null)
            {

                await App.Current.MainPage.DisplayAlert("error", "Log In failed please try again", "ok");
            }
            else
            {
                

                Page p = new NavigationPage(new Views.UserPage());
                App.Current.MainPage = p;

                //d
            }
        }
    }
}