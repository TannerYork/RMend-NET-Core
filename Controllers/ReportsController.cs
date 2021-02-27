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
        private readonly ReportContext _context;

        public ReportsController(ReportContext context)
        {
            _context = context;
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDTO>>> GetReports()
        {
            return await _context.Reports
                .Select(x => ReportToDTO(x))
                .ToListAsync();
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportDTO>> GetReport(long id)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return ReportToDTO(report);
        }

        // PUT: api/Reports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(long id, ReportDTO reportDTO)
        {
            if (id != reportDTO.Id)
            {
                return BadRequest();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            report.Name = reportDTO.Name;
            report.IsPriority = reportDTO.IsPriority;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ReportExists(id))
            {
                 return NotFound();
            }

            return NoContent();
        }

        // POST: api/Reports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportDTO>> PostReport(ReportDTO reportDTO)
        {
            var report = new Report
            {
                IsPriority = reportDTO.IsPriority,
                Name = reportDTO.Name
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetReport),
                new { id = report.Id },
                ReportToDTO(report));
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(long id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportExists(long id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }

        private static ReportDTO ReportToDTO(Report report) =>
            new ReportDTO
            {
                Id = report.Id,
                Name = report.Name,
                IsPriority = report.IsPriority
            };
    }
}
