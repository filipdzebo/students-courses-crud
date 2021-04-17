using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Page_Backend.Data;
using Page_Backend.Models;

namespace Page_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StudentsController(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            return await _context.Studenti.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Studenti.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> PutStudent(int id, StudentRequestModel student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }
            string status = "";
            int id_statusa = 0;
            switch (student.StatusStudenta)
            {
                case 1:
                    status = "Redovan";
                    id_statusa = 1;
                    break;
                case 2:
                    status = "Vanredan";
                    id_statusa = 2;
                    break;
            }
            Student updated_stud = await _context.Studenti.FirstAsync(x => x.StudentId == id);
            updated_stud.Ime = student.Ime;
            updated_stud.Prezime = student.Prezime;
            updated_stud.BrojIndeksa = student.BrojIndeksa;
            updated_stud.Godina = student.Godina;
            updated_stud.StatusStudenta = new StatusStudenta { StatusStudentaId = id_statusa, NazivStatusa = status };
            _context.Entry(updated_stud).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Student>> PostStudent(StudentRequestModel student)
        {
            string status = "";
            switch (student.StatusStudenta)
            {
                case 1:
                    status = "Redovan";
                    break;
                case 2:
                    status = "Vanredan";
                    break;
            }
            Student new_stud = new Student()
            {
                Ime = student.Ime,
                Prezime = student.Prezime,
                BrojIndeksa = student.BrojIndeksa,
                Godina = student.Godina,
                StatusStudenta = _context.Status_Studenta.Where(x => x.NazivStatusa == status).ToList()[0]
            };
            _context.Studenti.Add(new_stud);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }

        // DELETE: api/Students/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _context.Studenti.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            List<KursStudent> ks =await _context.KursStudenti.Where(x => x.StudentId == id).ToListAsync();
            foreach (KursStudent item in ks)
            {
                _context.KursStudenti.Remove(item);
                await _context.SaveChangesAsync();
            }
            _context.Studenti.Remove(student);
            await _context.SaveChangesAsync();

            var studenti = await _context.Detalji.FromSqlRaw("spGetStudents").ToListAsync();
            return Ok(studenti);
        }

        private bool StudentExists(int id)
        {
            return _context.Studenti.Any(e => e.StudentId == id);
        }
        [Authorize]
        [HttpGet("GetStudentsProcedure")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var studenti =await _context.Detalji.FromSqlRaw("spGetStudents").ToListAsync();
            return Ok(studenti);
        }
        [Authorize]
        [HttpGet("GetStudentByIdProcedure/{id}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentById(int id)
        {
            var student =await _context.Detalji.FromSqlRaw("spGetStudentById {0}",id).ToListAsync();
            return Ok(student);
        }

    }
}
