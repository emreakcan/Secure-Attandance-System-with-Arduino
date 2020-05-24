using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFIDAttendence.DTO;
using RFIDAttendence.Model;

namespace RFIDAttendence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceController : Controller
    {
        private readonly AttendenceDBContext _context;

        public AttendenceController(AttendenceDBContext context)    
        {
            _context = context;
        }
        public JsonResult Index()
        {
            return Json("1");
        }

        // GET: api/Attendence
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendence>>> GetAttendence()
        {

            var list = await _context.Attendence.ToListAsync();
            return list;
        }

        // GET: api/Attendence/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendence>> GetAttendence(int id)
        {
            var attendence = await _context.Attendence.FindAsync(id);

            if (attendence == null)
            {
                return NotFound();
            }

            return attendence;
        }

        // PUT: api/Attendence/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendence(int id, Attendence attendence)
        {
            if (id != attendence.AttendenceId)
            {
                return BadRequest();
            }

            _context.Entry(attendence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendenceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Attendence
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Attendence>> PostAttendence(AttendenceDTO attendence)

        {
            var student = _context.Student.Where(t => t.CardUID == attendence.UID && t.TCNo == attendence.TCNo).FirstOrDefault();
            if (student!=null)
            {
                
                _context.Attendence.Add(new Attendence
                {
                    CheckInDate = attendence.checkIn,
                    Student = student,
                    StudentId = student.StudentId
                });

                await _context.SaveChangesAsync();
                attendence.studentName = student.Name;
                attendence.studentSurname = student.Surname;
                return Json(attendence);
            }
            else return Json("0");
        }
        [HttpPost("PostStudent")]
        public async Task<ActionResult<Attendence>> PostStudent(Student student)

        {



            _context.Student.Add(student);
            await _context.SaveChangesAsync();
            return Json("1");

        }



        // DELETE: api/Attendence/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Attendence>> DeleteAttendence(int id)
        {
            var attendence = await _context.Attendence.FindAsync(id);
            if (attendence == null)
            {
                return NotFound();
            }

            _context.Attendence.Remove(attendence);
            await _context.SaveChangesAsync();

            return attendence;
        }

        private bool AttendenceExists(int id)
        {
            return _context.Attendence.Any(e => e.AttendenceId == id);
        }
    }
}
