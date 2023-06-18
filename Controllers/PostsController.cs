using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLSVAPI.Data;
using QLSVAPI.Models;
using QLSVAPI1.Models;
using QLSVAPI1.Models.DTOs;

namespace QLSVAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly QLSVAPIContext _context;

        public PostsController(QLSVAPIContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPost()
        {
          if (_context.Post == null)
          {
              return NotFound();
          }
            
            var list_post = await _context.Post.ToListAsync();
            var result = new List<PostDTO>();
            foreach (var post in list_post)
            {
                var postDTO = DataToDTO(post);
                result.Add(postDTO);
            }
            return result;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPostByUserId(int userId)
        {
            if (_context.Post == null)
            {
                return NotFound();
            }
            var result = new List<PostDTO>();
            var list_post = await _context.Post.Where(post => post.UserId == userId).ToListAsync();

            foreach (var post in list_post)
            {
                var postDTO = DataToDTO(post);
                result.Add(postDTO);
            }
            return Ok(new { post = result, status = 200 });
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
          if (_context.Post == null)
          {
              return NotFound();
          }
            var post = await _context.Post.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
          if (_context.Post == null)
          {
              return Problem("Entity set 'QLSVAPIContext.Post'  is null.");
          }
            _context.Post.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (_context.Post == null)
            {
                return NotFound();
            }
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return (_context.Post?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private PostDTO DataToDTO(Post post)
        {
            var result = new PostDTO
            {
                Id = post.Id,
                UserId = post.UserId,
                CreateAt = post.CreateAt,
                UpdateAt = post.UpdateAt,
                Title = post.Title,
                Thumbnail = post.Thumbnail,
            };

            return result;
        }
    }
}
