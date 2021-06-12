using pillApp.Models;
using pillApp.Services;
using pillApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace pillApp.ViewModels
{
    public class CourseTypePickerItem
    {
        public eCourseType Type;
        public string DisplayString;
    }
    public class CourseFreqItem
    {
        public eCourseFreq Freq;
        public string DisplayString;
    }

    [QueryProperty(nameof(CourseID), nameof(CourseID))]
    class EditCourseViewModel : BaseViewModel
    {
        public Command EditCourseCommand { get; set; }
        public Command AddCourseCommand { get; set; }
        public EditCourseViewModel()
        {
            EditCourseCommand = new Command(OnPressEdit);
            AddCourseCommand = new Command(OnAddCourseAsync);
        }
        public bool IsFreqNotEveryday
        {
            get => _isFreqNotEveryDay;
            set => SetProperty(ref _isFreqNotEveryDay, value);
        }
        public bool IsDurationNotEndless
        {
            get => _isDurationNotEndless;
            set => SetProperty(ref _isDurationNotEndless, value);
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public string CourseType
        {
            get => Globals.eCourseTypeToString[_courseType];
            set => SetProperty(ref _courseType, Globals.eCourseTypeFromString[value]);
        }
        public List<string> CourseTypes
        {
            get
            {
                var list = new List<string>();
                foreach (var key in Globals.eCourseTypeFromString.Keys)
                {
                    list.Add(key);
                }
                return list;
            }
        }
        public List<string> CourseFreqs
        {
            get
            {
                var list = new List<string>();
                foreach (var key in Globals.eCourseFreqFromString.Keys)
                {
                    list.Add(key);
                }
                return list;
            }
        }
        public List<string> CourseDurationTypes
        {
            get
            {
                var list = new List<string>();
                foreach (var key in Globals.eCourseDurationFromString.Keys)
                {
                    list.Add(key);
                }
                return list;
            }
        }
        public int CourseTypeSelectedIndex
        {
            get => _courseTypeSelectedIndex;
            set => SetProperty(ref _courseTypeSelectedIndex, value);
        }
        public int CourseFreqSelectedIndex
        {
            get => _courseFreqSelectedIndex;
            set
            {
                IsFreqNotEveryday = Globals.eCourseFreqFromString[CourseFreqs[value]] != eCourseFreq.EVERYDAY;
                SetProperty(ref _courseFreqSelectedIndex, value);
            }
        }
        public int CourseDurationSelectedIndex
        {
            get => _courseDurationSelectedIndex;
            set
            {
                IsDurationNotEndless = Globals.eCourseDurationFromString[CourseDurationTypes[value]] != eCourseDuration.ENDLESS;
            }
        }

        public string CourseFreqDays
        {
            get => _courseFreqDays.ToString();
            set => SetProperty(ref _courseFreqDays, int.Parse(value));
        }
        public string ReceptionCountInDay
        {
            get => _receptionCountInDay.ToString();
            set => SetProperty(ref _receptionCountInDay, int.Parse(value));
        }
        public string CourseTypeText
        {
            get => CourseTypes[_courseTypeSelectedIndex];
        }
        public string ReceptionUnitText
        {
            get => _receptionUnit;
            set => SetProperty(ref _receptionUnit, value);
        }
        public string ReceptionValue
        {
            get => _receptionValue.ToString();
            set => SetProperty(ref _receptionValue, float.Parse(value));
        }
        public string CourseID
        {
            get => _courseID;
            set
            {
                _courseID = value;
                if (!string.IsNullOrEmpty(value))
                {
                    LoadCourse();
                }
            }
        }
        private int _courseTypeSelectedIndex;
        private int _courseFreqSelectedIndex;
        private int _courseDurationSelectedIndex;

        // course data fields
        private string _courseID;
        private eFoodDependency _foodDependency;
        private eCourseType _courseType;
        private eCourseFreq _courseFreq;
        private int _receptionCountInDay;
        private int _courseFreqDays;
        private int _duration;
        private float _receptionValue;
        private string _receptionUnit;
        private string _name;
        private string _description;
        private DateTime _startDate = DateTime.Now;
        private bool _isFreqNotEveryDay;
        private bool _isDurationNotEndless;

        private void LoadCourse()
        {
            try
            {
                var course = CoursesDataStore.GetItem(_courseID).Result;

                CourseTypeSelectedIndex = CourseTypes.FindIndex(item => item == Globals.eCourseTypeToString[course.CourseType]);
                //TODO add picker for food dependency
                //FoodDependency = course.FoodDependency;
                CourseFreqSelectedIndex = CourseFreqs.FindIndex(item => item == Globals.eCourseFreqToString[course.CourseFreq]);
                //ReceptionCountInDay = course.ReceptionCountInDay.ToString();
                //Duration = course.Duration;
                //ReceptionValue = course.ReceptionValue;
                //ReceptionUnitText = course.ReceptionUnit;
                Name = course.Name;
                Description = course.Description;
                CourseType = Globals.eCourseTypeToString[course.CourseType];
                _startDate = DateTime.Now;
            }
            catch (Exception)
            {

            }

        }

        private async void OnAddCourseAsync()
        {
            // код создания экземпляра класса Course и его добавление в табличку
            await Shell.Current.GoToAsync("../..");
        }

        private async void OnPressEdit()
        {
            await Shell.Current.GoToAsync(
                $"{nameof(EditCoursePage)}" +
                $"?{nameof(CourseID)}={CourseID}");
        }
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
