using System.ComponentModel.DataAnnotations;

namespace QLSVAPI.Models.DTOs
{
    public class GiangVienDTO
    {

        [Key]
        public int Id { get; set; }
        public string TenGV { get; set; }
        public string MaGV { get; set; }
        public DateTime NgaySinh { get; set; }

        public string GioiTinh { get; set; }

        public int KhoaId { get; set; }

        public string Email { get; set; }
        //  public string Secret { get; set; }

        public string TenKhoa { get; set; }
    }
}
