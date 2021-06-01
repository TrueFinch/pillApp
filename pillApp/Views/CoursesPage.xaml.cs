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
    public partial class CoursesPage : ContentPage
    {
        CoursesViewModel _viewModel;
        public CoursesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CoursesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}