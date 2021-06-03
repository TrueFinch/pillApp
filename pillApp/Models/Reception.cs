using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace pillApp.Models
{
    class Reception
    {
        [PrimaryKey, AutoIncrement]
        public Guid ID { get; set; }
        public Guid CourseID { get; set; }
        public DateTime DateTime { get; set; }
        public bool isAccepted { get; set; }
    }
}
