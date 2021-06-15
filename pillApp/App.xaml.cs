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

            //DependencyService.Register<MockDataStore>();
            DependencyService.Register<CoursesDataStore>();
            MainPage = new AppShell();
            // this piece of code is not working at all! I do not understand
            NotificationCenter.Current.NotificationReceived += OnLocalNotificationReceived;
            //
            NotificationCenter.Current.NotificationTapped += OnLocalNotificationTapped;
        }

        private void OnLocalNotificationTapped(NotificationTappedEventArgs args)
        {
            //debug code
            if (args.Request.NotificationId == -666)
            {
                return;
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                MainPage.Navigation.PushModalAsync(new AlertPage(args.Request.NotificationId));
            });
        }
        private void OnLocalNotificationReceived(NotificationReceivedEventArgs args)
        {
            lastNotificationID = args.Request.NotificationId;
            //debug code
            if (args.Request.NotificationId == -666)
            {
                return;
            }
            if (!isSleeping)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Shell.Current.Navigation.PushModalAsync(new AlertPage(args.Request.NotificationId));
                });
            } else
            {
                wasReceivedWhenSleeping = true;
            }
        }
        protected override void OnStart()
        {
            isSleeping = false;
            wasReceivedWhenSleeping = false;
        }

        protected override void OnSleep()
        {
            isSleeping = true;
        }

        protected override void OnResume()
        {
            isSleeping = false;
            if (wasReceivedWhenSleeping)
            {
                wasReceivedWhenSleeping = false;
                Device.BeginInvokeOnMainThread(() =>
                {
                    Shell.Current.Navigation.PushModalAsync(new AlertPage(lastNotificationID));
                });
            }
        }

        private bool isSleeping;
        private bool wasReceivedWhenSleeping;
        private int lastNotificationID;
    }
}
