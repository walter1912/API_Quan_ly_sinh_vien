using System.ComponentModel.DataAnnotations;

namespace QLSVAPI.Models
{
    public class GiangVien
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên giảng viên không được để trống")]
        public string TenGV { get; set; }

        [Required(ErrorMessage = "Mã giảng viên không được để trống")]
        public string MaGV { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống")]
        public string GioiTinh { get; set; }

        [Required(ErrorMessage = "KhoaId không được để trống")]
        public int KhoaId { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
    }
}

