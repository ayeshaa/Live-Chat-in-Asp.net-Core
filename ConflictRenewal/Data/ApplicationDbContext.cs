using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ConflictRenewal.Models;
using Microsoft.AspNetCore.Identity;

namespace ConflictRenewal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Conflict> Conflict { get; set; }
        public DbSet<Journal> Journal { get; set; }
        public DbSet<AuditTrailTable> AuditTrailTable { get; set; }
    }

}
