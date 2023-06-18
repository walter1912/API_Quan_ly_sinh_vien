using System.ComponentModel.DataAnnotations;
using BCrypt.Net;

namespace QLSVAPI1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string Username { get; set; }
        //username là mã sinh viên hoặc mã giảng viên

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        public int Role { get; set; }
        //role là vị trí, 1 là giảng viên, 2 là sinh viên
    }
}
