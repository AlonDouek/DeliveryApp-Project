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
    public partial class ResMenuPage : ContentPage
    {
        public ResMenuPage()
        {
            this.BindingContext = new ResMenuPageViewModel();
            InitializeComponent();
        }
    }
}