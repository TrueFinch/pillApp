using System;
using System.Collections.Generic;
using pillApp.ViewModels;
using pillApp.Views;
using Xamarin.Forms;

namespace pillApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(EditCoursePage), typeof(EditCoursePage));
            Routing.RegisterRoute(nameof(CourseDetailPage), typeof(CourseDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
