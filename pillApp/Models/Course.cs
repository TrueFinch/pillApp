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
        DAYS_OF_WEAK,   // определённые дни недели
        EVERY_N_DAY,    // через каждый N дней
    }
    [Flags]
    public enum eDaysOfWeek
    {
        SUN = 0,
        MON = 1,
        TUE = 2,
        WED = 4,
        THU = 8,
        FRI = 16,
        SAT = 32,
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
        AFTER,
    }
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public Guid ID { get; set; }
        public eFoodDependency FoodDependency { get; set; }
        public eCourseType CourseType { get; set; }
        public eCourseFreq CourseFreq { get; set; }
        public eDaysOfWeek DaysOfWeek { get; set; }
        public int ReceptionCountInDay { get; set; }
        public int Duration { get; set; }
        public float ReceptionValue { get; set; }
        public string ReceptionUnit { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastFetchDate { get; set; }
    }

}
