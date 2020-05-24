using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFIDAttendence.DTO
{
    public class AttendenceDTO
    {
        public int lectureId { get; set; }
        public string UID { get; set; }
        public string TCNo { get; set; }
        public DateTime checkIn { get; set; }
        public string? studentName { get; set; }
        public string? studentSurname{ get; set; }


    }
}
