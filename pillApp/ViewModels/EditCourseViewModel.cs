using pillApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace pillApp.ViewModels
{
    [QueryProperty(nameof(Course), nameof(Course))]
    class EditCourseViewModel: BaseViewModel
    {
        public bool IsView { get; set; }
        public bool IsEdit { get => !IsView; }
        public string CourseNameText
        {
            get => _course.Name;
            set => _course.Name = value;
        }
        public string CourseDescriptionText
        {
            get => _course.Description;
            set => _course.Description = value;
        }
        public string Test
        {
            get { return String.Empty; }
            set
            {
                Console.WriteLine(value.ToString());
            }
        }
        public Course Course
        { 
            get => _course;
            set
            {
                _course = value;
            }
        }
        private Course _course = new Course();
        //public EditCourseViewModel()
        //{
        //    if (_course == null)
        //    {
        //        _course = new Course();
        //        IsView = false;
        //    } else
        //    {
        //        IsView = true;
        //    }
        //}
    }
}
