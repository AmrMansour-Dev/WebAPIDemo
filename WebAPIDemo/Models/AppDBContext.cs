using Microsoft.EntityFrameworkCore;

namespace WebAPIDemo.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }  

    }
}
