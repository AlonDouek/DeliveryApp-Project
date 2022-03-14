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
    public partial class TEMPVIEW1 : ContentPage
    {
        public TEMPVIEW1()
        {
            InitializeComponent();
        }
        private void goToU(object sender, EventArgs e)
        {
            App.Current.MainPage = new UserPage();
        }
    }
}