using pillApp.Models;
using pillApp.Services;
using pillApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace pillApp.ViewModels
{
    public class ReceptionTimePicker
    {
        public TimeSpan Time { get; set; }
    }
    [QueryProperty(nameof(CourseID), nameof(CourseID))]
    class EditCourseViewModel : BaseViewModel
    {
        public Command EditCourseCommand { get; set; }
        public Command AddCourseCommand { get; set; }
        public EditCourseViewModel()
        {
            EditCourseCommand = new Command(OnPressEdit);
            AddCourseCommand = new Command(OnSaveCourseAsync);
            ReceptionTimePickers = new ObservableCollection<ReceptionTimePicker>();
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
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    SetProperty(ref _courseType, Globals.eCourseTypeFromString[value]);
                }
            }
        }
        public string CourseFreq
        {
            get => Globals.eCourseFreqToString[_courseFreq];
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    SetProperty(ref _courseFreq, Globals.eCourseFreqFromString[value]);
                }
            }
        }
        public string CourseDuration
        {
            get => Globals.eCourseDurationToString[_courseDuration];
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    SetProperty(ref _courseDuration, Globals.eCourseDurationFromString[value]);
                }
            }
        }
        public string FoodDependency
        {
            get => Globals.eCourseFoodDependencyToString[_foodDependency];
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    SetProperty(ref _foodDependency, Globals.eCourseFoodDependencyFromString[value]);
                }
            }
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
        public List<string> FoodDepTypes
        {
            get
            {
                var list = new List<string>();
                foreach (var key in Globals.eCourseFoodDependencyFromString.Keys)
                {
                    list.Add(key);
                }
                return list;
            }
        }
        public ObservableCollection<ReceptionTimePicker> ReceptionTimePickers
        {
            get => _receptionTimePickers;
            set => SetProperty(ref _receptionTimePickers, value);
        }
        public int CourseTypeSelectedIndex
        {
            get => _courseTypeSelectedIndex;
            set
            {
                CourseType = CourseTypes[value];
                SetProperty(ref _courseTypeSelectedIndex, value);
            }
        }
        public int CourseFreqSelectedIndex
        {
            get => _courseFreqSelectedIndex;
            set
            {
                CourseFreq = CourseFreqs[value];
                IsFreqNotEveryday = _courseFreq != eCourseFreq.EVERYDAY;
                if (_courseFreq == eCourseFreq.EVERYDAY)
                {
                    CourseFreqDays = "1";
                }
                SetProperty(ref _courseFreqSelectedIndex, value);
            }
        }
        public int CourseDurationSelectedIndex
        {
            get => _courseDurationSelectedIndex;
            set
            {
                CourseDuration = CourseDurationTypes[value];
                IsDurationNotEndless = _courseDuration != eCourseDuration.ENDLESS;
                SetProperty(ref _courseDurationSelectedIndex, value);
            }
        }
        public int FoodDepSelectedIndex
        {
            get => _foodDepSelectedIndex;
            set
            {
                FoodDependency = FoodDepTypes[value];
                SetProperty(ref _foodDepSelectedIndex, value);
            }
        }

        public string CourseFreqDays
        {
            get => _courseFreqDays.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                SetProperty(ref _courseFreqDays, int.Parse(value));
            }
        }
        public string ReceptionCountInDay
        {
            get => _receptionCountInDay.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                var intVal = int.Parse(value);
                if (intVal > ReceptionTimePickers.Count)
                {
                    var count = intVal - ReceptionTimePickers.Count;
                    for (var i = 0; i < count; ++i)
                    {
                        ReceptionTimePickers.Add(new ReceptionTimePicker
                        {
                            Time = new TimeSpan(0, 0, 0),
                        });
                    }
                    ReceptionTimePickers = new ObservableCollection<ReceptionTimePicker>(ReceptionTimePickers.OrderBy(x => x.Time).ToList());
                }
                else if (intVal < ReceptionTimePickers.Count)
                {
                    var count = ReceptionTimePickers.Count - intVal;
                    for (var i = 0; i < count; ++i)
                    {
                        ReceptionTimePickers.RemoveAt(0);
                    }
                    ReceptionTimePickers = new ObservableCollection<ReceptionTimePicker>(ReceptionTimePickers.OrderBy(x => x.Time).ToList());
                }
                SetProperty(ref _receptionCountInDay, int.Parse(value));
            }
        }
        public string Duration
        {
            get => _duration.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                SetProperty(ref _duration, int.Parse(value));
            }
        }
        public string ReceptionValue
        {
            get => _receptionValue.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                SetProperty(ref _receptionValue, float.Parse(value));
            }
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
        private ObservableCollection<ReceptionTimePicker> _receptionTimePickers;

        private int _courseTypeSelectedIndex;
        private int _courseFreqSelectedIndex;
        private int _courseDurationSelectedIndex;
        private int _foodDepSelectedIndex;
        // course data fields
        private string _courseID;
        private string _name;
        private string _description;
        private eCourseType _courseType;
        private eCourseFreq _courseFreq;
        private eCourseDuration _courseDuration;
        private eFoodDependency _foodDependency;
        private int _courseFreqDays = 1;
        private int _receptionCountInDay;
        private int _duration;
        private float _receptionValue;
        private DateTime _startDate = DateTime.Now;
        private DateTime _lastFetchDate = DateTime.Now;
        // bools for IsVisible
        private bool _isFreqNotEveryDay;
        private bool _isDurationNotEndless;

        private void LoadCourse()
        {
            try
            {
                var course = dataStore.GetCourse(_courseID);

                Name = course.Name;
                Description = course.Description;

                CourseTypeSelectedIndex = CourseTypes.FindIndex(
                    item => item == Globals.eCourseTypeToString[course.CourseType]
                );
                CourseFreqSelectedIndex = CourseFreqs.FindIndex(
                    item => item == Globals.eCourseFreqToString[course.CourseFreq]
                );
                CourseDurationSelectedIndex = CourseDurationTypes.FindIndex(
                    item => item == Globals.eCourseDurationToString[course.CourseDuration]
                );
                FoodDepSelectedIndex = FoodDepTypes.FindIndex(
                    item => item == Globals.eCourseFoodDependencyToString[course.FoodDependency]
                );
                CourseFreqDays = course.CourseFreqDays.ToString();
                Duration = course.Duration.ToString();
                ReceptionValue = course.ReceptionValue.ToString();
                _startDate = DateTime.Now;
                _lastFetchDate = course.LastFetchDate;
                var times = dataStore.GetReceptionsTimes(_courseID);
                ReceptionCountInDay = times.Count.ToString();
                for (var i = 0; i < times.Count; ++i)
                {
                    ReceptionTimePickers[i].Time = times[i];
                }
            }
            catch (Exception)
            {

            }

        }

        private async void OnSaveCourseAsync()
        {
            var times = new List<TimeSpan>();
            foreach (var picker in ReceptionTimePickers)
            {
                times.Add(picker.Time);
            }
            if (string.IsNullOrEmpty(_courseID))
            {
                var newCourse = GetCourse();
                dataStore.AddCourse(newCourse, times);
            }
            else
            {
                var course = GetCourse();
                dataStore.UpdateCourse(course, times);
            }
            // код создания экземпляра класса Course и его добавление в табличку
            await Shell.Current.GoToAsync("../..");
        }

        private async void OnPressEdit()
        {
            await Shell.Current.GoToAsync(
                $"{nameof(EditCoursePage)}" +
                $"?{nameof(CourseID)}={CourseID}");
        }
        public void SortTimePickers()
        {
            var sorted = new ObservableCollection<ReceptionTimePicker>(ReceptionTimePickers.OrderBy(x => x.Time).ToList());
            for (var i = 0; i < sorted.Count; ++i)
            {
                if (ReceptionTimePickers[i].Time != sorted[i].Time)
                {
                    ReceptionTimePickers = sorted;
                    return;
                }
            }
        }
        private Course GetCourse()
        {
            return new Course
            {
                ID = string.IsNullOrEmpty(_courseID) ? Guid.NewGuid().ToString() : _courseID,
                Name = _name,
                Description = _description,
                CourseType = Globals.eCourseTypeFromString[CourseTypes[_courseTypeSelectedIndex]],
                CourseFreq = Globals.eCourseFreqFromString[CourseFreqs[_courseFreqSelectedIndex]],
                CourseDuration = Globals.eCourseDurationFromString[CourseDurationTypes[_courseDurationSelectedIndex]],
                FoodDependency = Globals.eCourseFoodDependencyFromString[FoodDepTypes[_foodDepSelectedIndex]],
                CourseFreqDays = _courseFreqDays,
                Duration = _duration,
                ReceptionValue = _receptionValue,
                StartDate = _startDate,
                LastFetchDate = _lastFetchDate,
            };
        }
    }
}
