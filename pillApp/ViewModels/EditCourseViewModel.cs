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
            EditCourseCommand = new Command(
                async () =>
                {
                    await Shell.Current.GoToAsync(
                        $"{nameof(EditCoursePage)}" +
                        $"?{nameof(CourseID)}={CourseID}");
                });
            AddCourseCommand = new Command(OnAddCourseAsync);
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
        List<string> _courseTypes = new List<string> {
            "Pill"    ,
            "Big pill",
            "Drops"   ,
            "Potion"  ,
            "Salve"   ,
            "Injection",
            "Procedure",
        };
        List<CourseFreqItem> _courseFreqs = new List<CourseFreqItem>
        {
            new CourseFreqItem{ Freq = eCourseFreq.EVERYDAY,     DisplayString = "Everyday"     },
            new CourseFreqItem{ Freq = eCourseFreq.DAYS_OF_WEAK, DisplayString = "Days of weak" },
            new CourseFreqItem{ Freq = eCourseFreq.EVERY_N_DAY,  DisplayString = "Every N day"  },
        };
        public List<string> CourseTypes
        {
            get => _courseTypes;
        }
        public List<CourseFreqItem> CourseFreqs
        {
            get => _courseFreqs;
        }
        public int CourseTypeSelectedIndex
        {
            get => _courseTypeSelectedIndex;
            set => SetProperty(ref _courseTypeSelectedIndex, value);
        }
        public int CourseFreqSelectedIndex
        {
            get => _courseFreqSelectedIndex;
            set => SetProperty(ref _courseFreqSelectedIndex, value);
        }
        public bool IsView
        {
            get => _isView;
            set => SetProperty(ref _isView, value);
        }
        public bool IsEdit
        {
            get => !_isView;
            set => SetProperty(ref _isView, !value);
        }
        public string ReceptionCountInDay
        {
            get => _receptionCountInDay.ToString();
            set => SetProperty(ref _receptionCountInDay, int.Parse(value));
        }
        public string CourseTypeText
        {
            get => _courseTypes[_courseTypeSelectedIndex];
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
        private bool _isView;
        // course data fields
        private string _courseID;
        private eFoodDependency _foodDependency;
        private eCourseType _courseType;
        //private eCourseFreq _courseFreq;
        private eDaysOfWeek _daysOfWeek;
        private int _receptionCountInDay;
        private int _duration;
        private float _receptionValue;
        private string _receptionUnit;
        private string _name;
        private string _description;
        private DateTime _startDate = DateTime.Now;
        private void LoadCourse()
        {
            try
            {
                var course = CoursesDataStore.GetItem(_courseID).Result;

                //CourseTypeSelectedIndex = _courseTypes.FindIndex(item => item.Type == course.CourseType);
                //TODO add picker for food dependency
                //FoodDependency = course.FoodDependency;
                CourseFreqSelectedIndex = _courseFreqs.FindIndex(item => item.Freq == course.CourseFreq);
                //DaysOfWeek = course.DaysOfWeek;
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
            await Shell.Current.GoToAsync(
                "../..");
        }

        private void OnPressEdit()
        {
            IsView = false;
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
