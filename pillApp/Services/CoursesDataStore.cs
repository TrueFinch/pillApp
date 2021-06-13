using System;
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
            populateData();
            //
            var list = database.Table<Course>().ToList();
        }

        public void AddItem(Course item, List<TimeSpan> receptionTimes)
        {
            var rowID = Guid.NewGuid().ToString();
            item.ID = rowID;
            database.Insert(item);
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
            if (item.CourseFreq == eCourseFreq.EVERY_N_DAY)
            {
                //TODO create many receptions for course
            }
        }

        public void DeleteItem(string id)
        {
            database.Delete<Course>(id);
        }

        public Course GetItem(string id)
        {
            return database.Get<Course>(id);
        }

        public Task<IEnumerable<Course>> GetItems(bool forceRefresh = false)
        {
            var courses = database.Table<Course>().ToList();
            return Task.FromResult<IEnumerable<Course>>(courses);
        }

        public void UpdateItem(Course item, List<TimeSpan> receptionTimes)
        {
            database.Update(item);
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

        private void populateData()
        {
            var course1 = new Course
            {
                Name = "Нурофен",
                Description = "обезболвающее",
                CourseType = eCourseType.PILL,
                CourseFreq = eCourseFreq.EVERYDAY,
                FoodDependency = eFoodDependency.NO_MATTER,
                ReceptionCountInDay = 2,
                Duration = 10,
                ReceptionValue = 1,
                StartDate = DateTime.Now,
                LastFetchDate = DateTime.Now,
            };
            //database.Insert(course1);

            AddItem(course1, new List<TimeSpan>
            {
                new TimeSpan(8, 0, 0),
                new TimeSpan(18, 0, 0),
            });
        }
    }
}
