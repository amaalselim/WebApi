using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.Models
{
    public class ITIEntity : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> employees { get; set; }
        public ITIEntity()
        {
             
        }
        public ITIEntity(DbContextOptions options):base(options) 
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=WebApi;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        }
    }
}
