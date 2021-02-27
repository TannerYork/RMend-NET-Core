using System;
using Microsoft.EntityFrameworkCore;
namespace RMendAPI.Models
{
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions<ReportContext> options)
            : base(options)
        {
        }

        public DbSet<Report> Reports { get; set; }
    }
}
