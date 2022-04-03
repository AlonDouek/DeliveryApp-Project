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

        #region props
       
        private string creditCard;

        public string CreditCard
        {
            get { return creditCard; }

            set
            {
                creditCard = value;
                creditCard = "************" + creditCard.Substring(12);
                OnPropertyChanged("CreditCard");
            }
        }

        #endregion

        private User user;
        public User User
        {
            get { return user; }
            set
            {
                if (user != value)
                {
                    user = value;
                    OnPropertyChanged("User");
                }
            }
        }



        public ICommand ChangeCredentialCommand { protected set; get; }
        public ICommand LogoutCommand { protected set; get; }

        public UserPageViewModel()
        {
            int bp = 0;
            User = ((App)App.Current).CurrentUser;
            CreditCard = User.CreditCard;
            //CreditCard = "0000000000000000";
            ChangeCredentialCommand = new Command(MoveChangeCredential);
            LogoutCommand = new Command(Logout);
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

                
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("error", "failed, please Log In", "ok");
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.LogInPage());

            }
        }
        #region fff

        public async void Logout()
        {
            try
            {
                DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();
                if (User != null)
                {
                    App App = (App)Application.Current;
                    App.NullCurrentUser();

                    //bool sp = await proxy.LogoutAsync();

                    //User a = ((App)App.Current).CurrentUser;
                    //int ssd = 0;
                    
                    await App.Current.MainPage.Navigation.PushModalAsync(new Views.LogInPage());
                }
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("error", "that wasnt suppose to happen Hhmmm...", "ok");
            }

        }

        #endregion
    }
}
