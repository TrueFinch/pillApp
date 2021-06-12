using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace pillApp.Models
{
    public enum eCourseType
    {
        PILL,       // таблетка
        BIG_PILL,   // пилюля
        DROPS,      // капли
        POTION,     // микстура
        SALVE,      // мазь
        INJECTION,  // укол
        PROCEDURE,  // процедура
    }
    public enum eCourseFreq
    {
        EVERYDAY,       // каждый день
        EVERY_N_DAY,    // через каждый N дней
    }
    public enum eCourseDuration
    {
        ENDLESS,        // постоянно
        N_DAYS,         // N дней
        N_RECEPTIONS,   // N приёмов
    }
    public enum eFoodDependency
    {
        NO_MATTER,
        BEFORE,
        WHILE,
        AFTER,
    }
    [Table("Courses")]
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public eCourseType CourseType { get; set; }
        public eCourseFreq CourseFreq { get; set; }
        public eCourseDuration CourseDuration { get; set; }
        public eFoodDependency FoodDependency { get; set; }
        public int CourseFreqDays { get; set; }
        public int ReceptionCountInDay { get; set; }
        public int Duration { get; set; }
        public float ReceptionValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastFetchDate { get; set; }
    }
}
