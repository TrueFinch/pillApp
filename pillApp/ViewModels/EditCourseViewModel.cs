using pillApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace pillApp.ViewModels
{
    class EditCourseViewModel: BaseViewModel
    {
        public bool IsReadOnly { get; set; }
        public bool IsNotReadOnly { get; set; }
        public Course Course
        { 
            get => _course;
            set
            {
                IsReadOnly = value != null;
                IsNotReadOnly = value == null;
                _course = value;
            }
        }
        private Course _course;
        public EditCourseViewModel()
        {
            IsReadOnly = true;
        }
    }
}
