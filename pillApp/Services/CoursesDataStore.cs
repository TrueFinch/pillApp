using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using pillApp.Models;
using SQLite;

namespace pillApp.Services
{
    public class CoursesDataStore
    {
        readonly SQLiteConnection database;
        string dbName = "pill.db";
        public CoursesDataStore()
        {
            var dbPath = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal),
                dbName
            );
            database = new SQLiteConnection(dbPath);
            database.CreateTable<Course>();
            database.CreateTable<Reception>();
            database.CreateTable<ReceptionsTime>();
            //TODO comment it on release
            _ = database.DeleteAll<Course>();
            _ = database.DeleteAll<Reception>();
            _ = database.DeleteAll<ReceptionsTime>();
            populateData();
            //
            var list = database.Table<Course>().ToList();
        }

        public void AddCourse(Course item, List<TimeSpan> receptionTimes)
        {
            var rowID = Guid.NewGuid().ToString();
            item.ID = rowID;
            //insert reception times per day
            foreach (var time in receptionTimes)
            {
                database.Insert(new ReceptionsTime
                {
                    ID = Guid.NewGuid().ToString(),
                    CourseID = rowID,
                    Time = time,
                });
            }
            InitReceptions(ref item);
            database.Insert(item);
        }

        public void DeleteCourse(string id)
        {
            database.Delete<Course>(id);
            database.Table<ReceptionsTime>().Where(
                x => x.CourseID == id
            ).Delete();
            database.Table<Reception>().Where(
                x => x.CourseID == id
            ).Delete();
        }

        public Course GetCourse(string id)
        {
            return database.Get<Course>(id);
        }

        public List<Course> GetCourses()
        {
            var courses = database.Table<Course>().ToList();
            return courses;
        }

        public void UpdateCourse(Course item, List<TimeSpan> receptionTimes)
        {
            database.Table<ReceptionsTime>().Where(x => x.CourseID == item.ID).Delete();
            foreach (var time in receptionTimes)
            {
                database.Insert(new ReceptionsTime
                {
                    ID = Guid.NewGuid().ToString(),
                    CourseID = item.ID,
                    Time = time,
                });
            }
            UpdateReceptions(ref item, DateTime.Today);
            database.Update(item);
        }

        public List<TimeSpan> GetReceptionsTimes(string id)
        {
            var times = database.Table<ReceptionsTime>().Where(x => x.CourseID == id).ToList();
            var res = new List<TimeSpan>();
            foreach (var time in times)
            {
                res.Add(time.Time);
            }
            return res;
        }
        public List<Reception> GetReceptionsByDate(DateTime date)
        {
            date = date.Date;
            var receptions = database.Table<Reception>().ToList();
            var times = database.Table<Reception>().Where(
                x => x.ClearDate == date).ToList();
            return times;
        }
        private void populateData()
        {
            var course1 = new Course
            {
                Name = "Нурофен",
                Description = "обезболвающее",
                CourseType = eCourseType.PILL,
                CourseFreq = eCourseFreq.EVERYDAY,
                CourseDuration = eCourseDuration.N_DAYS,
                FoodDependency = eFoodDependency.NO_MATTER,
                CourseFreqDays = 2,
                Duration = 10,
                ReceptionValue = 1,
                StartDate = DateTime.Now,
                LastFetchDate = DateTime.Now,
            };
            AddCourse(course1, new List<TimeSpan>
            {
                new TimeSpan(8, 0, 0),
                new TimeSpan(18, 0, 0),
            });
            var course2 = new Course
            {
                Name = "Боль",
                Description = "Страдание",
                CourseType = eCourseType.INJECTION,
                CourseFreq = eCourseFreq.EVERYDAY,
                CourseFreqDays = 1,
                CourseDuration = eCourseDuration.ENDLESS,
                FoodDependency = eFoodDependency.AFTER,
                ReceptionValue = 100,
                StartDate = DateTime.Now,
                LastFetchDate = DateTime.Now,
            };
            AddCourse(course2, new List<TimeSpan>
            {
                new TimeSpan(12, 0, 0),
            });
        }
        public void FetchReceptions(Course course, DateTime fetchDate)
        {
            if (course.CourseDuration != eCourseDuration.ENDLESS)
            {
                return;
            }
            var recTimes = GetReceptionsTimes(course.ID);
            var courseID = course.ID;
            var recCount = CountTotalReceptionsDays(
                            course.LastFetchDate.Date,
                            fetchDate.Date,
                            course.CourseFreqDays)
                        * recTimes.Count;
            course.LastFetchDate = GenerateReceptions(
                course,
                course.LastFetchDate.AddDays(course.CourseFreqDays),
                recCount,
                recTimes
            );
            database.Update(course);

        }
        private int CountTotalReceptionsDays(DateTime from, DateTime to, int N)
        {
            //from date not included (but implied that it has receptions)
            //N -  days beetwen receptions (every N day)
            return (((int)(to - from).TotalDays) / N);
        }
        private void UpdateReceptions(ref Course course, DateTime updatedFromDate)
        {
            var recTimes = GetReceptionsTimes(course.ID);
            var courseID = course.ID;
            var recCount = 0;
            var date = updatedFromDate.Date;
            var courseStart = course.StartDate.Date;
            database.Table<Reception>().Where(
                x => (x.CourseID == courseID) && (((x.DateTime >= date) || (x.DateTime < courseStart)))
            ).Delete();
            DateTime lastDay = course.StartDate.Date;
            var oldRecs = database.Table<Reception>()
                .Where(x => x.CourseID == courseID)
                .OrderByDescending(x => x.ClearDate)
                .ToList();
            if (oldRecs.Count != 0)
            {
                lastDay = oldRecs[0].ClearDate.AddDays(course.CourseFreqDays);
            }
            switch (course.CourseDuration)
            {
                case eCourseDuration.ENDLESS:
                    recCount = (CountTotalReceptionsDays(
                            lastDay.Date,
                            course.LastFetchDate.Date,
                            course.CourseFreqDays) + 1)
                        * recTimes.Count;
                    break;
                case eCourseDuration.N_DAYS:
                    var passedDays = CountTotalReceptionsDays(
                        course.StartDate.Date,
                        updatedFromDate.Date,
                        course.CourseFreqDays);
                    recCount = (course.Duration - passedDays) * recTimes.Count;
                    break;
                case eCourseDuration.N_RECEPTIONS:
                    recCount = course.Duration - oldRecs.Count;
                    break;
            }
            course.LastFetchDate = GenerateReceptions(course, lastDay, recCount, recTimes);
        }
        //should be called on course creation
        private void InitReceptions(ref Course course)
        {
            var recTimes = GetReceptionsTimes(course.ID);
            var recCount = 0;
            DateTime lastDay = course.StartDate;
            switch (course.CourseDuration)
            {
                case eCourseDuration.ENDLESS:
                    recCount = 10 * recTimes.Count;
                    break;
                case eCourseDuration.N_DAYS:
                    recCount = course.Duration * recTimes.Count;
                    break;
                case eCourseDuration.N_RECEPTIONS:
                    recCount = course.Duration;
                    break;
            }
            course.LastFetchDate = GenerateReceptions(course, course.StartDate.Date, recCount, recTimes);
        }
        public DateTime GenerateReceptions(Course course, DateTime start, int recCount, List<TimeSpan> recTimes)
        {
            DateTime lastDay = start;
            while (recCount > 0)
            {
                for (var i = 0; i < recTimes.Count && recCount > 0; ++i)
                {
                    DateTime newRecTime = lastDay.Date + recTimes[i];
                    database.Insert(new Reception
                    {
                        ID = Guid.NewGuid().ToString(),
                        CourseID = course.ID,
                        DateTime = newRecTime,
                        ClearDate = newRecTime.Date,
                        isAccepted = newRecTime < DateTime.Now,
                    });
                    --recCount;
                }
                if (recCount > 0)
                {
                    lastDay = lastDay.AddDays(course.CourseFreqDays);
                }
            }
            // last fetched day (usefull only for endless)
            return lastDay;
        }
    }
}
