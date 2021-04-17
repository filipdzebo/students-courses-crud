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
    public class KursController : ControllerBase
    {
        private readonly DataContext _context;

        public KursController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Kurs
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Kurs>>> GetKursevi()
        {
            return await _context.Kursevi.ToListAsync();
        }

        // GET: api/Kurs/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Kurs>> GetKurs(int id)
        {
            var kurs = await _context.Kursevi.FindAsync(id);

            if (kurs == null)
            {
                return NotFound();
            }

            return kurs;
        }

        // PUT: api/Kurs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKurs(int id, Kurs kurs)
        {
            if (id != kurs.KursId)
            {
                return BadRequest();
            }

            _context.Entry(kurs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KursExists(id))
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

        // POST: api/Kurs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Kurs>> PostKurs(KursStudentRequestModel kurs)
        {
            Kurs new_kurs = new Kurs() { Naziv = kurs.Naziv };
            _context.Kursevi.Add(new_kurs);
            await _context.SaveChangesAsync();
            new_kurs = await _context.Kursevi.FirstOrDefaultAsync(x => x.Naziv == kurs.Naziv);
            for (int i = 0; i < kurs.OdabraniStudenti.Count; i++)
            {
                KursStudent new_ks = new KursStudent() { KursId = new_kurs.KursId, StudentId = kurs.OdabraniStudenti[i] };
                _context.KursStudenti.Add(new_ks);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetKurs", new { id = kurs.KursId }, kurs);
        }

        // DELETE: api/Kurs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Kurs>> DeleteKurs(int id)
        {
            var kurs = await _context.Kursevi.FindAsync(id);
            if (kurs == null)
            {
                return NotFound();
            }

            _context.Kursevi.Remove(kurs);
            await _context.SaveChangesAsync();

            return kurs;
        }

        private bool KursExists(int id)
        {
            return _context.Kursevi.Any(e => e.KursId == id);
        }
    }
}
