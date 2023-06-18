using Microsoft.EntityFrameworkCore;
using QLSVAPI.Models;
using QLSVAPI1.Models;

namespace QLSVAPI.Data
{
    public class QLSVAPIContext : DbContext
    {
        public QLSVAPIContext (DbContextOptions<QLSVAPIContext> options)
            : base(options)
        {
          
        }

      
        public DbSet<SinhVien> SinhVien { get; set; } = default!;
        public DbSet<Khoa> Khoa { get; set; } = default!;
        public DbSet<GiangVien> GiangVien { get; set; } = default!;
        public DbSet<QLSVAPI1.Models.User> User { get; set; } = default!;
        public DbSet<QLSVAPI1.Models.Post> Post { get; set; } = default!;
        public DbSet<QLSVAPI1.Models.Comment> Comment { get; set; } = default!;
        public DbSet<QLSVAPI1.Models.Favorite> Favorite { get; set; } = default!;
    }
}
