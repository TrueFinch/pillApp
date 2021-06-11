using pillApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace pillApp.ViewModels
{
    public enum ePageMode
    {
        VIEW,
        EDIT,
    }

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
    [QueryProperty(nameof(PageMode), nameof(PageMode))]
    class EditCourseViewModel : BaseViewModel
    {
        List<CourseTypePickerItem> _courseTypes = new List<CourseTypePickerItem> {
            new CourseTypePickerItem{ Type = eCourseType.PILL,      DisplayString = "Pill"      },
            new CourseTypePickerItem{ Type = eCourseType.BIG_PILL,  DisplayString = "Big pill"  },
            new CourseTypePickerItem{ Type = eCourseType.DROPS,     DisplayString = "Drops"     },
            new CourseTypePickerItem{ Type = eCourseType.POTION,    DisplayString = "Potion"    },
            new CourseTypePickerItem{ Type = eCourseType.SALVE,     DisplayString = "Salve"     },
            new CourseTypePickerItem{ Type = eCourseType.INJECTION, DisplayString = "Injection" },
            new CourseTypePickerItem{ Type = eCourseType.PROCEDURE, DisplayString = "Procedure" },
        };
        List<CourseFreqItem> _courseFreqs = new List<CourseFreqItem>
        {
            new CourseFreqItem{ Freq = eCourseFreq.EVERYDAY,     DisplayString = "Everyday"     },
            new CourseFreqItem{ Freq = eCourseFreq.DAYS_OF_WEAK, DisplayString = "Days of weak" },
            new CourseFreqItem{ Freq = eCourseFreq.EVERY_N_DAY,  DisplayString = "Every N day"  },
        };
        public List<CourseTypePickerItem> CourseTypes
        {
            get => _courseTypes;
        }
        public List<CourseFreqItem> CourseFreqs
        {
            get => _courseFreqs;
        }
        public int SelectedIndex { get; set; }
        public bool IsView { get => _mode == ePageMode.VIEW; }
        public bool IsEdit { get => _mode == ePageMode.EDIT; }
        public string CountText
        {
            get => _course == null ? "" : _course.ReceptionCountInDay.ToString();
        }
        public string CourseTypeText
        {
            get
            {
                if (_course == null)
                {
                    return string.Empty;
                }
                return _courseTypes.Find(item => item.Type == _course.CourseType).DisplayString;
            }
        }
        public string ReceptionUnitText
        {
            get => _course == null ? "" : _course.ReceptionUnit;
            set
            {
                if (_course != null)
                {
                    _course.ReceptionUnit = value;
                }
            }
        }
        public string CourseNameText
        {
            get => _course == null ? "" : _course.Name;
            set 
            {
                if (_course != null)
                {
                    _course.Name = value;
                }
            }
        }
        public string CourseDescriptionText
        {
            get => _course == null ? "" : _course.Description;
            set
            {
                if (_course != null)
                {
                    _course.Description = value;
                }
            }
        }
        public string CourseID
        {
            get => _courseID;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _course = new Course();
                } else
                {
                    _course = CoursesDataStore.GetItem(value).Result;
                }
            }
        }
        public string PageMode
        {
            get
            {
                return _mode.ToString();
            }
            set
            {
                if (Enum.TryParse(value, out ePageMode mode))
                {
                    _mode = mode;
                }
            }
        }
        private ePageMode _mode;
        private string _courseID;
        private Course _course;
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
