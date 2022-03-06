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
    class LogInPageViewModel
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region props

        #region email
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
                ValidatePassword();
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



        #endregion

        
        private string error;

        public string Error
        {
            get => Error;
            set
            {
                Error = value;
                OnPropertyChanged("error");
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
           
            if (string.IsNullOrEmpty(Password) || (Password.Length < 5 || Password.Length > 30))
            {
                this.passwordError = "must type password between 5-30 character!";
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

            if (string.IsNullOrEmpty(Email) || !(Email.Contains("@") && Email.EndsWith(".com")))
            {
                this.emailError = "must type correct Email";
                this.ShowEmailError = true; 
            }
            else
            {
                this.emailError = "";
                this.ShowEmailError = false;
            }
                
        }

        public ICommand SubmitCommand { protected set; get; }

        public LogInPageViewModel()
        {
         
            PasswordError = "";
            ShowPasswordError = false;
            EmailError = "";
            ShowEmailError = false;
            SubmitCommand = new Command(OnSubmit);
        }

        private bool ValidateForm()
        {
            if (((App)App.Current).CurrentUser != null)
            {
                error = "already logged in";
                return false;
            }

            ValidateEmail();
            ValidatePassword();

            return !((ShowEmailError && ShowPasswordError) || (ShowEmailError || ShowPasswordError));
        }

        public async void OnSubmit()
        {
            try
            {
                DeliveryAPIProxy proxy = DeliveryAPIProxy.CreateProxy();
                bool yep = ValidateForm();
                if (yep)
                {
                    User user = await proxy.LoginAsync(Email, Password);
                    if (user == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Login failed, please check and try again", "OK");
                    }
                    else
                    {
                        App theApp = (App)Application.Current;
                        theApp.CurrentUser = user;
                        int ads = 2;
                        //App.Current.MainPage = new UserPage();
                        App.Current.MainPage = new TabsMP();
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Login Failed", "You did everything wrong", "Okay");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            
            

        }



    }
    }


