using System.ComponentModel.DataAnnotations;

namespace QLSVAPI.Models
{

        public class SinhVien
        {
        [Key]
            public int Id { get; set; }
        [Required(ErrorMessage = "Tên sinh viên không được để trống")]
        public string TenSV { get; set; }

        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        public string MaSV { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống")]
        public string GioiTinh { get; set; }

        [Required(ErrorMessage = "KhoaId không được để trống")]
        public int KhoaId { get; set; }

        [Required(ErrorMessage = "GiangVienId không được để trống")]
        public int GiangVienId { get; set; }
    }
    //  public string Secret { get; set; }


}
