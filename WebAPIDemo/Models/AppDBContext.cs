using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPIDemo.Models
{
    public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Departement> Departement { get; set; }


    }
}
