using pillApp.Models;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace pillApp.Services
{
    class NotificationSystem
    {
        //public CoursesDataStore dataStore => CoursesDataStore.Instance;
        public static NotificationSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NotificationSystem();
                }
                return _instance;
            }
        }
        private NotificationSystem()
        {
        }

        public int CreateNotification(Reception rec, Course course)
        {
            //var course = dataStore.GetCourse(rec.CourseID);
            var id = nextNotificationID++;
            NotificationCenter.Current.Show(new NotificationRequest
            {
                NotificationId = id,
                Title = "Time to get " + course.Name,
                Description = $"{course.ReceptionValue:0.0} {Globals.eCourseTypeToString[course.CourseType]}",
                Schedule = new NotificationRequestSchedule
                {
                    Repeats = NotificationRepeat.TimeInterval,
                    NotifyTime = rec.DateTime,
                    NotifyRepeatInterval = RepeatAfter,
                    NotifyAutoCancelTime = rec.DateTime + RepeatAfter * RepeatCount
                }
            });
            return id;
        }
        public void CancelNotification(int notificationID)
        {
            NotificationCenter.Current.Cancel(notificationID);
        }

        private static NotificationSystem _instance = new NotificationSystem();
        private static int nextNotificationID;
        private static readonly TimeSpan RepeatAfter = new TimeSpan(0, 10, 0);
        private static readonly int RepeatCount = 3;
    }
}
