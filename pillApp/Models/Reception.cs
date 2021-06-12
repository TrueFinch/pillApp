using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace pillApp.Models
{
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
        public bool isAccepted { get; set; }
    }
}
