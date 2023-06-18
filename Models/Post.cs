using System.ComponentModel.DataAnnotations;

namespace QLSVAPI1.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; } 

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        [Required(ErrorMessage = "Tiêu đề bài viết không được để trống")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Nội dung bài viết không được để trống")]
        public string Content { get; set; } 

        public string Thumbnail { get; set; }

    }
}
