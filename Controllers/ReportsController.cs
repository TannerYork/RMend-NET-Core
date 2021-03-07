using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMendAPI.Models;

namespace RMendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReport>>> GetReports()
        {
            return await _context.Reports
                .Select(x => ReportToDTO(x))
                .ToListAsync();
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReport>> GetReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return ReportToDTO(report);
        }

        // POST: api/Reports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserReport>> PostReport(UserReport userReport)
        {
            var authority = await _context.Authorities.FindAsync(userReport.AuthorityId);
            if (authority == null)
            {
                return NotFound();
            }

            var report = new Report
            {
                Name = userReport.Name,
                IsPriority = false,
                AuthorityId = authority.AuthorityId,
                Authority = authority
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            report = await _context.Reports.FindAsync(report.ReportId);
            authority.Reports.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetReport),
                new { id = report.ReportId },
                ReportToDTO(report));
        }

        private bool ReportExists(long id)
        {
            return _context.Reports.Any(e => e.ReportId == id);
        }

        private static UserReport ReportToDTO(Report report) =>
            new UserReport
            {
                ReportId = report.ReportId,
                Name = report.Name
            };
    }
}
