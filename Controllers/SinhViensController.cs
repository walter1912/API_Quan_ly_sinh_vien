using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLSVAPI.Data;
using QLSVAPI.Models;

namespace QLSVAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinhViensController : ControllerBase
    {
        private readonly QLSVAPIContext _context;

        public SinhViensController(QLSVAPIContext context)
        {
            _context = context;
        }

        // GET: api/SinhViens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SinhVien>>> GetSinhVien()
        {
          if (_context.SinhVien == null)
          {
              return NotFound();
          }
            return await _context.SinhVien.ToListAsync();
        }

        // GET: api/SinhViens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SinhVien>> GetSinhVien(int id)
        {
          if (_context.SinhVien == null)
          {
              return NotFound();
          }
            var sinhVien = await _context.SinhVien.FindAsync(id);

            if (sinhVien == null)
            {
                return NotFound();
            }

            return sinhVien;
        }
        [HttpGet("maSV/{maSV}")]
        public async Task<ActionResult<SinhVien>> GetSinhVienByMaGV(string maSV)
        {
            if (_context.SinhVien == null)
            {
                return NotFound();
            }
            var sinhVien = await _context.SinhVien.FirstOrDefaultAsync((sv)=> sv.MaSV == maSV);

            if (sinhVien == null)
            {
                return NotFound();
            }

            return Ok(new { sinhVien, status = 200 });
        }
        [HttpGet("khoa/{khoaId}")]
        public async Task<ActionResult<IEnumerable<SinhVien>>> GetSinhVienByKhoaId(int khoaId)
        {
            if (_context.SinhVien == null)
            {
                return NotFound();
            }
            var sinhVien = await _context.SinhVien.Where(sv => sv.KhoaId == khoaId).ToListAsync();

            if (sinhVien == null)
            {
                return NotFound();
            }

            return Ok(new { sinhVien, status = 200 });
        }
        [HttpGet("giangvien/{giangVienId}")]
        public async Task<ActionResult<IEnumerable<SinhVien>>> GetSinhVienByGiangVienId(int giangVienId)
        {
            if (_context.SinhVien == null)
            {
                return NotFound();
            }
            var sinhVien = await _context.SinhVien.Where(sv => sv.GiangVienId == giangVienId).ToListAsync();

            if (sinhVien == null)
            {
                return NotFound();
            }

            return Ok(new { sinhVien, status = 200 });
        }
        // PUT: api/SinhViens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSinhVien(int id, SinhVien sinhVien)
        {
            if (id != sinhVien.Id)
            {
                return BadRequest();
            }

            _context.Entry(sinhVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SinhVienExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { status = 201 });
        }

        // POST: api/SinhViens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SinhVien>> PostSinhVien(SinhVien sinhVien)
        {
          if (_context.SinhVien == null)
          {
              return Problem("Entity set 'QLSVAPIContext.SinhVien'  is null.");
          }
            _context.SinhVien.Add(sinhVien);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSinhVien", new { id = sinhVien.Id }, sinhVien);
        }

        // DELETE: api/SinhViens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSinhVien(int id)
        {
            if (_context.SinhVien == null)
            {
                return NotFound();
            }
            var sinhVien = await _context.SinhVien.FindAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            _context.SinhVien.Remove(sinhVien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SinhVienExists(int id)
        {
            return (_context.SinhVien?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
