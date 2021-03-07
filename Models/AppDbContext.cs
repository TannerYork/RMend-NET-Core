using System;
using Microsoft.EntityFrameworkCore;
namespace RMendAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Authority> Authorities { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
