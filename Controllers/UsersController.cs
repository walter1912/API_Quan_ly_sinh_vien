using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DiaSymReader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NuGet.ContentModel;
using QLSVAPI.Data;
using QLSVAPI1.Models;
using QLSVAPI1.Models.DTOs;
using Xunit;

namespace QLSVAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly QLSVAPIContext _context;

        private readonly IConfiguration _configuration;

        public UsersController(QLSVAPIContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }   
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUser()
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            var listUser = await _context.User.ToListAsync();

            var result = new List<UserDTO>();

            foreach(var user in listUser)
            {
                var userDto = DataToDTO(user);
                result.Add(userDto);
            }

            return result;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            var result = DataToDTO(user);
            return result;
        }
        // GET: api/Users/B19DCCN638
        [HttpGet("username/{username}")]
        public async Task<ActionResult<UserDTO>> GetUser(string username)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            var result = DataToDTO(user);
            return result;
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> CheckLogin(UserDTO request)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == request.Username);
            
            if(user == null)
            {
                return NotFound("Không tìm thấy user");
            }
            if(!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Mật khẩu bị sai.");
            }
            var result = DataToDTO(user);
            return Ok(new { user = result, status = 200 });
          
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value
                    ));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private ActionResult<User> Ok(User user, int StatusCode)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

    

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(UserDTO request)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user != null)
            {
                return NotFound(new { mes = "Đã tồn tại username, yêu cầu nhập username khác" });
            }
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);


            var user2 = new User
                {
                    Username = request.Username,
                    Role = request.Role,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };
           
            _context.User.Add(user2);
            await _context.SaveChangesAsync();
            var result = DataToDTO(user2);
            return Ok(new {user = result, mes = "Đăng ký thành công"});

        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private UserDTO DataToDTO(User user)
        {
            var result = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role
            };

           return result;

        }
    }
}
