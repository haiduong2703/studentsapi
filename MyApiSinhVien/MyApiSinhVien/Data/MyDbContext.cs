using Microsoft.EntityFrameworkCore;

namespace MyApiSinhVien.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        #region DBset   
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<Khoa> Khoas { get; set; }
        #endregion

        
    }
}
