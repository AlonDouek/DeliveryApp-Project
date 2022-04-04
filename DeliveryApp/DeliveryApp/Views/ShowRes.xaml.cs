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
    public partial class ShowRes : ContentPage
    {
        public ShowRes()
        {
            this.BindingContext = new showResViewModel();
            InitializeComponent();
        }
    }
}