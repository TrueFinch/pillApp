using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using pillApp.Models;
using pillApp.Views;
using Plugin.LocalNotification;
using Xamarin.Forms;

namespace pillApp.ViewModels
{
    class CoursesViewModel : BaseViewModel
    {
        private Course _selectedCourse;
        public ObservableCollection<Course> Courses { get; }
        public Command LoadCoursesCommand { get; }
        public Command AddCourseCommand { get; }
        public Command<Course> CourseTapped { get; }
        public Command DebugNotifyCommand { get; }
        public CoursesViewModel()
        {
            Title = "Курсы";
            Courses = new ObservableCollection<Course>();
            LoadCoursesCommand = new Command(async () => ExecuteLoadCoursesCommand());
            AddCourseCommand = new Command(OnAddCourse);
            CourseTapped = new Command<Course>(OnCourseSelected);
            DebugNotifyCommand = new Command(() =>
            {
                var notification = new NotificationRequest
                {
                    NotificationId = -666,
                    BadgeNumber = 1,
                    Title = "Test",
                    Description = "Notification",
                    ReturningData = "",
                    Schedule = new NotificationRequestSchedule
                    {
                        Repeats = NotificationRepeat.No,
                        NotifyTime = DateTime.Now.AddSeconds(5)
                    }
                };
                NotificationCenter.Current.Show(notification);
            });
        }

        void ExecuteLoadCoursesCommand()
        {
            IsBusy = true;
            try
            {
                Courses.Clear();
                var courses = dataStore.GetCourses();
                foreach (var course in courses)
                {
                    Courses.Add(course);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        internal void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Course SelectedItem
        {
            get => _selectedCourse;
            set
            {
                SetProperty(ref _selectedCourse, value);
                OnCourseSelected(value);
            }
        }

        private async void OnAddCourse(object obj)
        {
            await Shell.Current.GoToAsync(
                $"{nameof(EditCoursePage)}" +
                $"?{nameof(EditCourseViewModel.CourseID)}={string.Empty}"
            );
        }

        async void OnCourseSelected(Course course)
        {
            if (course == null)
            {
                return;
            }
            //// This will push the EditCoursePage onto the navigation stack
            await Shell.Current.GoToAsync(
                $"{nameof(CourseDetailPage)}" +
                $"?{nameof(EditCourseViewModel.CourseID)}={course.ID}"
            );
        }
    }
}
