using DeliveryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeliveryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {

            SignUpPageViewModel context = new SignUpPageViewModel();
            context.Push += (p) => Navigation.PushAsync(p);
            this.BindingContext = context;

            InitializeComponent();
        }
        private void goToUserPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserPage());
        }
    }
}