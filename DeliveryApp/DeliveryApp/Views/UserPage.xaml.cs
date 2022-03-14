using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeliveryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        public UserPage()
        {
            this.BindingContext = new UserPageViewModel();
            InitializeComponent();
        }
        private void LogIn(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LogInPage());
        }

        
    }
}