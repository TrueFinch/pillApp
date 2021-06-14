using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using pillApp.Models;
using pillApp.Services;
using Xamarin.Forms;

namespace pillApp.ViewModels
{
    public class ReceptionVisual
    {
        public Reception Model { get; set; }
        public Course Course { get; set; }
        public string CourseType { get; set; }
    }
    public class ReceptionsViewModel : BaseViewModel
    {
        public ReceptionsViewModel()
        {
            Title = "Приёмы";
            Receptions = new ObservableCollection<ReceptionVisual>();
            LoadReceptionsCommand = new Command(LoadReceptions);
            CurrentDate = DateTime.Now;
        }
        internal void OnAppearing()
        {
            IsBusy = true;
        }
        public void LoadReceptions()
        {
            IsBusy = true;
            try
            {
                Receptions.Clear();
                var courses = dataStore.GetCourses();
                foreach (var course in courses)
                {
                    if (course.CourseDuration == eCourseDuration.ENDLESS)
                    {
                        if (course.LastFetchDate <= CurrentDate)
                        {
                            dataStore.FetchReceptions(course, CurrentDate);
                        }
                    }
                }
                var receprions = dataStore.GetReceptionsByDate(CurrentDate);
                receprions.Sort((a, b) => a.DateTime.CompareTo(b.DateTime));
                foreach (var rec in receprions)
                {
                    var course = courses.Find(x => x.ID == rec.CourseID);
                    Receptions.Add(
                        new ReceptionVisual {
                            Model = rec,
                            Course = course,
                            CourseType = Globals.eCourseTypeToString[course.CourseType],
                        }
                    );
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
        public ObservableCollection<ReceptionVisual> Receptions { get; }
        public Command LoadReceptionsCommand { get; }

        public DateTime CurrentDate
        {
            get => _currentDate;
            set
            {
                SetProperty(ref _currentDate, value);
                LoadReceptions();
            }
        }

        private DateTime _currentDate;
    }
}
