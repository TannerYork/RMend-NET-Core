using System;
using Microsoft.EntityFrameworkCore;
namespace RMendAPI.Models
{
    public class AuthorityContext : DbContext
    {
        public AuthorityContext(DbContextOptions<AuthorityContext> options)
            : base(options)
        {
        }

        public DbSet<Authority> Authorities { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}