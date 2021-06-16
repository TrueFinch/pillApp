using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace pillApp.Models
{
    [Table("Notifications")]
    public class Notification
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string ReceptionID { get; set; }
    }

    [Table("ReceptionsTimes")]
    public class ReceptionsTime
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string CourseID { get; set; }
        public TimeSpan Time { get; set; }
    }
    [Table("Receptions")]
    public class Reception
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string CourseID { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime ClearDate { get; set; }
        public bool isAccepted { get; set; }
    }
}
