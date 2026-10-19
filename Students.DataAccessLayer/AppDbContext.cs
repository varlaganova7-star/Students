using Microsoft.EntityFrameworkCore;
using Students.Model;

namespace Students.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Берем строку из единого конфигурационного класса
            optionsBuilder.UseSqlServer(DatabaseConfig.ConnectionString);
        }
    }
}