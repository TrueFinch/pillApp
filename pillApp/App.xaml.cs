using System;
using pillApp.Services;
using pillApp.Views;
using Plugin.LocalNotification;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pillApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<CoursesDataStore>();

            MainPage = new AppShell();

            NotificationCenter.Current.NotificationReceived += OnLocalNotificationReceived;
            NotificationCenter.Current.NotificationTapped += OnLocalNotificationTapped;
        }

        private void OnLocalNotificationTapped(NotificationTappedEventArgs args)
        {
            //debug code
            //if (args.Request.NotificationId == -666)
            //{
            //    return;
            //}
            Device.BeginInvokeOnMainThread(() =>
            {
                MainPage.Navigation.PushModalAsync(new AlertPage(args.Request.NotificationId));
            });
        }
        private void OnLocalNotificationReceived(NotificationReceivedEventArgs args)
        {
            //debug code
            //if (args.Request.NotificationId == -666)
            //{
            //    return;
            //}
            if (!isSleeping)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Shell.Current.Navigation.PushModalAsync(new AlertPage(args.Request.NotificationId));
                });
            }
        }
        protected override void OnStart()
        {
            isSleeping = false;
        }

        protected override void OnSleep()
        {
            isSleeping = true;
        }

        protected override void OnResume()
        {
            isSleeping = false;
        }

        private bool isSleeping;
    }
}
