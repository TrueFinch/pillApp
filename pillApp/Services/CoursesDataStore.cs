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
        readonly List<Course> courses;
        readonly List<Reception> receptions;
        string dbName;
        public CoursesDataStore()
        {
            var dbPath = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal),
                dbName
            );
            database = new SQLiteConnection(dbPath);
            if (database.Table<Course>() == null)
            {
                database.CreateTable<Course>();
                database.CreateTable<Reception>();
            }
        }

        public Task<bool> AddItem(Course item)
        {
            var rowID = new Guid();
            database.Insert(item);
            if (item.CourseFreq == eCourseFreq.DAYS_OF_WEAK 
                || item.CourseFreq == eCourseFreq.EVERY_N_DAY)
            {

            }

            return Task.FromResult(true);
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Course>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(Course item)
        {
            throw new NotImplementedException();
        }

        //private void populateData()
        //{
        //    var course1 = new Course {
        //        ID = 0,
        //        Name = "Нурофен",
        //        CourseType = eCourseType.PILL,
        //        CourseFreq = eCourseFreq.DAYS_OF_WEAK,
        //        DaysOfWeek = eDaysOfWeek.MON | eDaysOfWeek.FRI,
        //        FoodDependency = eFoodDependency.NO_MATTER,
        //        ReceptionCountInDay = 2,
        //        Duration = 10,
        //        ReceptionValue = 1,
        //        ReceptionUnit = "штука",
        //        StartDate = DateTime.Now,
        //        LastFetchDate = DateTime.Now,
        //    };
        //    ad
        //}
    }
}
