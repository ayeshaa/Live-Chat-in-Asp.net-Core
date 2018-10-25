using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ConflictRenewal.Models;

namespace ConflictRenewal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ConflictRenewal.Models.Conflict> Conflict { get; set; }
        public DbSet<ConflictRenewal.Models.Journal> Journal { get; set; }
    }
}
