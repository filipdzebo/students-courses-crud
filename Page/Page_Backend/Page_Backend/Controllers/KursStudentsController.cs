using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Page_Backend.Data;
using Page_Backend.Models;

namespace Page_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KursStudentsController : ControllerBase
    {
        private readonly DataContext _context;

        public KursStudentsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/KursStudents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KursStudent>>> GetKursStudenti()
        {
            return await _context.KursStudenti.ToListAsync();
        }

        // GET: api/KursStudents/id
        [HttpGet("sk/{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<KursStudent>>> GetStudentiKurs(int id)
        {
            List<KursStudent> ks =await _context.KursStudenti.Where(x => x.KursId == id).ToListAsync();
            List<StudentViewModel> studenti = new List<StudentViewModel>();
            for (int i = 0; i < ks.Count; i++)
            {
                Student student = await _context.Studenti.FirstOrDefaultAsync(x => x.StudentId == ks[i].StudentId);
                studenti.Add(new StudentViewModel {
                    StudentId=student.StudentId,
                    BrojIndeksa=student.BrojIndeksa,
                    Ime=student.Ime,
                    Prezime=student.Prezime,
                    Godina=student.Godina,
                });
            }
            return Ok(studenti.ToList());
        }

        // GET: api/KursStudents/id
        [HttpGet("ks/{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<KursStudent>>> GetKurseviStudent(int id)
        {
            List<KursStudent> ks = await _context.KursStudenti.Where(x => x.StudentId == id).ToListAsync();
            List<KursViewModel> kursevi = new List<KursViewModel>();
            for (int i = 0; i < ks.Count; i++)
            {
                Kurs kurs = await _context.Kursevi.FirstOrDefaultAsync(x => x.KursId == ks[i].KursId);
                kursevi.Add(new KursViewModel
                {
                    Naziv=kurs.Naziv
                });
            }
            return Ok(kursevi.ToList());
        }

        // GET: api/KursStudents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KursStudent>> GetKursStudent(int id)
        {
            var kursStudent = await _context.KursStudenti.FindAsync(id);

            if (kursStudent == null)
            {
                return NotFound();
            }

            return kursStudent;
        }

        // PUT: api/KursStudents/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKursStudent(int id, KursStudent kursStudent)
        {
            if (id != kursStudent.KursId)
            {
                return BadRequest();
            }

            _context.Entry(kursStudent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KursStudentExists(id))
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

        // POST: api/KursStudents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<KursStudent>> PostKursStudent(KursStudent kursStudent)
        {
            _context.KursStudenti.Add(kursStudent);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KursStudentExists(kursStudent.KursId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKursStudent", new { id = kursStudent.KursId }, kursStudent);
        }

        // DELETE: api/KursStudents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KursStudent>> DeleteKursStudent(int id)
        {
            var kursStudent = await _context.KursStudenti.FindAsync(id);
            if (kursStudent == null)
            {
                return NotFound();
            }

            _context.KursStudenti.Remove(kursStudent);
            await _context.SaveChangesAsync();

            return kursStudent;
        }

        private bool KursStudentExists(int id)
        {
            return _context.KursStudenti.Any(e => e.KursId == id);
        }
    }
}
