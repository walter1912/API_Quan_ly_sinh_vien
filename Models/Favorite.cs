using System.ComponentModel.DataAnnotations;

namespace QLSVAPI1.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "id bài viết không được để trống")]

        public int PostId { get; set; } 

        public int UserId { get; set; } 

        public DateTime CreatAt { get; set; } = DateTime.Now;

        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public int Type { get; set; }
        // 1 là like 
        //2 là unlike



    }
}
