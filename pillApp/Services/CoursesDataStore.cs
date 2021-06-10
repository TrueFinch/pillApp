using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using pillApp.Models;
using SQLite;

namespace pillApp.Services
{
    class CoursesDataStore : IDataStore<Course>
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
            _ = database.DeleteAll<Course>();
            populateData();
            var list = database.Table<Course>().ToList();
        }

        public Task<bool> AddItem(Course item)
        {
            var rowID = new Guid();
            database.Insert(item);
            if (item.CourseFreq == eCourseFreq.DAYS_OF_WEAK 
                || item.CourseFreq == eCourseFreq.EVERY_N_DAY)
            {
                //TODO create many receptions for course
            }

            return Task.FromResult(true);
        }

        public Task<bool> DeleteItem(string id)
        {
            database.Delete<Course>(id);
            return Task.FromResult(true);
        }

        public Task<Course> GetItem(string id)
        {
            return Task.FromResult(database.Get<Course>(id));
        }

        public Task<IEnumerable<Course>> GetItems(bool forceRefresh = false)
        {
            var courses = database.Table<Course>().ToList();
            return Task.FromResult<IEnumerable<Course>>(courses);
        }

        public Task<bool> UpdateItem(Course item)
        {
            throw new NotImplementedException();
        }

        private void populateData()
        {
            var course1 = new Course
            {
                ID = new Guid(),
                Name = "Нурофен",
                Description = "обезболвающее",
                CourseType = eCourseType.PILL,
                CourseFreq = eCourseFreq.DAYS_OF_WEAK,
                DaysOfWeek = eDaysOfWeek.MON | eDaysOfWeek.FRI,
                FoodDependency = eFoodDependency.NO_MATTER,
                ReceptionCountInDay = 2,
                Duration = 10,
                ReceptionValue = 1,
                ReceptionUnit = "штука",
                StartDate = DateTime.Now,
                LastFetchDate = DateTime.Now,
            };
            database.Insert(course1);
        }
    }
}
