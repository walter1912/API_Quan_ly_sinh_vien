﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLSVAPI.Data;
using QLSVAPI1.Models;

namespace QLSVAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly QLSVAPIContext _context;

        public FavoritesController(QLSVAPIContext context)
        {
            _context = context;
        }

        // GET: api/Favorites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetFavorite()
        {
          if (_context.Favorite == null)
          {
              return NotFound();
          }
            return await _context.Favorite.ToListAsync();
        }

        // GET: api/Favorites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Favorite>> GetFavorite(int id)
        {
          if (_context.Favorite == null)
          {
              return NotFound();
          }
            var favorite = await _context.Favorite.FindAsync(id);

            if (favorite == null)
            {
                return NotFound();
            }

            return favorite;
        }

        // GET: api/Favorites/user/userId
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetFavoriteByUserId(int userId)
        {
            if (_context.Favorite == null)
            {
                return NotFound();
            }
            var favorite = await _context.Favorite.Where(favorite => favorite.UserId == userId).ToListAsync();

            if (favorite == null)
            {
                return NotFound();
            }

            return favorite;
        }
        // GET: api/Favorites/post/postId
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetFavoriteByPostId(int postId)
        {
            if (_context.Favorite == null)
            {
                return NotFound();
            }
            var favorite = await _context.Favorite.Where(favorite => favorite.PostId == postId).ToListAsync();

            if (favorite == null)
            {
                return NotFound();
            }

            return favorite;
        }

        // PUT: api/Favorites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavorite(int id, Favorite favorite)
        {
            if (id != favorite.Id)
            {
                return BadRequest();
            }

            _context.Entry(favorite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteExists(id))
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

        // POST: api/Favorites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Favorite>> PostFavorite(Favorite favorite)
        {
          if (_context.Favorite == null)
          {
              return Problem("Entity set 'QLSVAPIContext.Favorite'  is null.");
          }
            _context.Favorite.Add(favorite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavorite", new { id = favorite.Id }, favorite);
        }


        [HttpPost("checkExist")]
        public async Task<ActionResult<Favorite>> CheckExisted(int userId,  int postId)
        {
            if (_context.Favorite == null)
            {
                return NotFound();
            }
            var result = await _context.Favorite.FirstOrDefaultAsync(u => u.UserId == userId && u.PostId == postId);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        // DELETE: api/Favorites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            if (_context.Favorite == null)
            {
                return NotFound();
            }
            var favorite = await _context.Favorite.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorite.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoriteExists(int id)
        {
            return (_context.Favorite?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
