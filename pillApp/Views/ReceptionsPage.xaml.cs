using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pillApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pillApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceptionsPage : ContentPage
    {
        ReceptionsViewModel _vm;
        public ReceptionsPage()
        {
            InitializeComponent();
            BindingContext = _vm = new ReceptionsViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.OnAppearing();
        }
    }

}