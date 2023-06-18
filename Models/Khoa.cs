using System.ComponentModel.DataAnnotations;

namespace QLSVAPI.Models
{
    public class Khoa
    {
        [Key]
        public int Id { get; set; }
        public string Ten { get; set; }
    }
}
