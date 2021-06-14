﻿using System;
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
            GenerateReceptions(ref item, item.StartDate);
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
            GenerateReceptions(ref item, DateTime.Today);
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
                Duration = 10,
                ReceptionValue = 1,
                StartDate = DateTime.Now,
                LastFetchDate = DateTime.Now,
            };
            //database.Insert(course1);

            AddCourse(course1, new List<TimeSpan>
            {
                new TimeSpan(8, 0, 0),
                new TimeSpan(18, 0, 0),
            });
        }
        public void FetchReceptions(Course course, DateTime fetchDate)
        {
            GenerateReceptions(ref course, fetchDate);
            database.Update(course);

        }
        private void GenerateReceptions(ref Course course, DateTime startDate)
        {
            var courseID = course.ID;
            var today = DateTime.Today;
            var t = database.Table<Reception>().Where(
                x => (x.CourseID == courseID) && (x.DateTime >= today)
            );
            t.Delete();
            var recTimes = GetReceptionsTimes(course.ID);
            var recCount = 0;
            switch (course.CourseDuration)
            {
                case eCourseDuration.ENDLESS:
                    recCount = 1000;
                    break;
                case eCourseDuration.N_DAYS:
                    recCount = course.Duration * recTimes.Count;
                    break;
                case eCourseDuration.N_RECEPTIONS:
                    recCount = course.Duration;
                    break;
            }
            DateTime lastDay = startDate.Date;
            while (recCount > 0)
            {
                course.LastFetchDate = lastDay;
                for (var i = 0; i < recTimes.Count && recCount > 0; ++i)
                {
                    DateTime newRecTime = lastDay.Date + recTimes[i];
                    database.Insert(new Reception
                    {
                        ID = Guid.NewGuid().ToString(),
                        CourseID = course.ID,
                        DateTime = newRecTime,
                        ClearDate = newRecTime.Date,
                        isAccepted = false
                    });
                    --recCount;
                }
                var daysToAdd = 1;
                if (course.CourseFreq == eCourseFreq.EVERY_N_DAY)
                {
                    daysToAdd = course.CourseFreqDays;
                }
                if (recCount > 0)
                {
                    lastDay = lastDay.AddDays(daysToAdd);
                }
            }
            course.LastFetchDate = lastDay;
        }
    }
}
