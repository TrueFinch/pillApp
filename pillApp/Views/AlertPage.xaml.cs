using pillApp.Services;
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
    public partial class AlertPage : ContentPage
    {
        public AlertPage(int notificationID)
        {
            _notificationID = notificationID;
            var recID = CoursesDataStore.Instance.GetReceptionIDByNotificationID(notificationID);
            var rec = CoursesDataStore.Instance.GetReceptionByID(recID);
            var course = CoursesDataStore.Instance.GetCourse(rec.CourseID);
            BindingContext = _vm = new AlertViewModel(notificationID);
            InitializeComponent();
            TitleLabel.Text = $"Time to get \"{course.Name}\"!!!";
            Description.Text = $"{course.ReceptionValue:0.0} — {Globals.eCourseTypeToString[course.CourseType]}";

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            _vm.ComfirmAlert(_notificationID);
            await Shell.Current.GoToAsync("../..");
        }
        private int _notificationID;
        private AlertViewModel _vm;
    }
}