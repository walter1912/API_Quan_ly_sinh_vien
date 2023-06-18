using System.ComponentModel.DataAnnotations;

namespace QLSVAPI1.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "id bài viết không được để trống")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "id user đăng bài không được để trống")]
        public int UserId { get; set; }

        public int RepCommentId { get; set; }

        [Required(ErrorMessage = "Nội dung comment không được để trống")]
        public string content { get; set; }

        public DateTime createAt { get; set; }

        public int Level { get; set; }  
    }
}
