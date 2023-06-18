namespace QLSVAPI.Models.DTOs
{
    public class SinhVienDTO
    {
        public int Id { get; set; }
        public string TenSV { get; set; }
        public string MaSV { get; set; }
        public DateTime NgaySinh { get; set; }

        public string GioiTinh { get; set; }

        public int KhoaId { get; set; }
        public bool IsEdit { get; set; }

        public int GiangVienId { get; set; }
        //  public string Secret { get; set; }
        public string TenKhoa { get; set; } 
      
    }
}
