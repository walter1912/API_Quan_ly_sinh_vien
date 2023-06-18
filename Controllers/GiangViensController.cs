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
    public class GiangViensController : ControllerBase
    {
        private readonly QLSVAPIContext _context;

        public GiangViensController(QLSVAPIContext context)
        {
            _context = context;
        }

        // GET: api/GiangViens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiangVien>>> GetGiangVien()
        {
          if (_context.GiangVien == null)
          {
              return NotFound();
          }
            return await _context.GiangVien.ToListAsync();
        }

        // GET: api/GiangViens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GiangVien>> GetGiangVien(int id)
        {
          if (_context.GiangVien == null)
          {
              return NotFound();
          }
            var giangVien = await _context.GiangVien.FindAsync(id);

            if (giangVien == null)
            {
                return NotFound();
            }

            return giangVien;
        }
        [HttpGet("maGV/{maGV}")]
        public async Task<ActionResult<GiangVien>> GetGiangVienByMaGV(string maGV)
        {
            if (_context.GiangVien == null)
            {
                return NotFound();
            }
            var giangVien = await _context.GiangVien.FirstOrDefaultAsync(gv => gv.MaGV == maGV);

            if (giangVien == null)
            {
                return NotFound();
            }

            return Ok(new { giangVien, status = 200 });
        }
        [HttpGet("khoa/{khoaId}")]
        public async Task<ActionResult<IEnumerable<GiangVien>>> GetGiangVienByKhoaId(int khoaId)
        {
            if (_context.GiangVien == null)
            {
                return NotFound();
            }
            var giangVien = await _context.GiangVien.Where(gv => gv.KhoaId == khoaId).ToListAsync();

            if (giangVien == null)
            {
                return NotFound();
            }

            return Ok(new { giangVien, status = 200 });
        }
        // PUT: api/GiangViens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGiangVien(int id, GiangVien giangVien)
        {
            if (id != giangVien.Id)
            {
                return BadRequest();
            }

            _context.Entry(giangVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GiangVienExists(id))
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

        // POST: api/GiangViens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GiangVien>> PostGiangVien(GiangVien giangVien)
        {
          if (_context.GiangVien == null)
          {
              return Problem("Entity set 'QLSVAPIContext.GiangVien'  is null.");
          }
            var giangvien = await _context.GiangVien.FirstOrDefaultAsync(gv => gv.MaGV == giangVien.MaGV);
            if(giangvien != null)
            {
                return NotFound(new { mes = "Đã tồn tại mã GV, yêu cầu nhập mã GV khác" });
            }
            _context.GiangVien.Add(giangVien);
            await _context.SaveChangesAsync();

            return Ok(new { giangVien, mes = "Thêm giảng viên thành công"});
        }

        // DELETE: api/GiangViens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGiangVien(int id)
        {
            if (_context.GiangVien == null)
            {
                return NotFound();
            }
            var giangVien = await _context.GiangVien.FindAsync(id);
            if (giangVien == null)
            {
                return NotFound();
            }

            _context.GiangVien.Remove(giangVien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GiangVienExists(int id)
        {
            return (_context.GiangVien?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
