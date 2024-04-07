using HMS.Areas.Identity.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<Admin>
    {
        public DbSet<Admin> Admin { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
