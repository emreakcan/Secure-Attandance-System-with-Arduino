using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//namespace RFIDAttendence.Model
//{
//    public class AttendenceDBContext
//    {
//    }
//}


namespace RFIDAttendence.Model
{
    public class AttendenceDBContext : DbContext
    {
        public DbSet<Attendence> Attendence { get; set; }
        public DbSet<Student> Student { get; set; }
        //public DbSet<Lecture> Lecture { get; set; }

        public AttendenceDBContext(DbContextOptions<AttendenceDBContext> options)
            : base(options)
        { }
    }
}
