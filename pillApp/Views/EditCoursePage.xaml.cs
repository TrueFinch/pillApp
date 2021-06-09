using pillApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pillApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCoursePage : ContentPage
    {
        EditCourseViewModel _viewModel;
        public EditCoursePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new EditCourseViewModel();
        }
    }
}