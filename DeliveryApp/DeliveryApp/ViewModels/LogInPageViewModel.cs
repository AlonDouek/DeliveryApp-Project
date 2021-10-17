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
                OnPropertyChanged("Password");
            }
        }

        public ICommand SubmitCommand { protected set; get; }

        public LogInPageViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        public async void OnSubmit()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.UserPage(this));
            DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();
            User user = await proxy.LoginAsync(Email, Password);
            if (user == null)
            {
                
                await App.Current.MainPage.DisplayAlert("שגיאה", "התחברות נכשלה, בדוק שם משתמש וסיסמה ונסה שוב", "בסדר");
            }
            else
            {
                App theApp = (App)App.Current;
                theApp.CurrentUser = user;
                
                Page p = new NavigationPage(new Views.UserPage());
                App.Current.MainPage = p;
                

            }
        }

    }
}
