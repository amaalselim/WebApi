using Microsoft.EntityFrameworkCore;

namespace Assignment_1.Models
{
    public class Context : DbContext
    {
        public DbSet<Employee> employees { get; set; }
        public Context()
        {
            
        }
        public Context(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=WebApiAssignment1;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        }

    }
}
