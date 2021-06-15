using pillApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace pillApp.ViewModels
{
    public class AlertViewModel : BaseViewModel
    {
        public AlertViewModel(int notificationID)
        {

        }

        public void ComfirmAlert(int notificationID)
        {
            NotificationSystem.Instance.CancelNotification(notificationID);
            CoursesDataStore.Instance.DeleteNotification(notificationID);
        }
    }
}
