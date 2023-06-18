using System.ComponentModel.DataAnnotations;

namespace QLSVAPI1.Models.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public string Title { get; set; }

        public string Thumbnail { get; set; }

    }
}
