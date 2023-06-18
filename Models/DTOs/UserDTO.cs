namespace QLSVAPI1.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Username { get; set; }
        //username là mã sinh viên hoặc mã giảng viên
        public string Password { get; set; }
        public int Role { get; set; }
        //role là vị trí, 1 là giảng viên, 2 là sinh viên
    }
}
