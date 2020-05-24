using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace RFIDAttendence.Model
{
    public class Attendence
    {
        [Key]
        public int AttendenceId { get; set; }


        [ForeignKey("StudentId")]
        public int StudentId { get; set; }

        [IgnoreDataMember]
        public virtual Student Student { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckInDate { get; set; }


    }
}
