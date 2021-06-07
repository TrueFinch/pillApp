using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace pillApp.Models
{
    [Table("Receptions")]
    public class Reception
    {
        [PrimaryKey, AutoIncrement]
        public Guid ID { get; set; }
        public Guid CourseID { get; set; }
        public DateTime DateTime { get; set; }
        public bool isAccepted { get; set; }
    }
}
