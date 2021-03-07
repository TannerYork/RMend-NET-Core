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
    [Route("api/Authorities")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthorityController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Authority
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Authority>>> GetAuthorities()
        {
            return await _context.Authorities.ToListAsync();
        }

        // GET: api/Authority/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Authority>> GetAuthority(int id)
        {
            var authority = await _context.Authorities.FindAsync(id);

            if (authority == null)
            {
                return NotFound();
            }

            return authority;
        }

        // PUT: api/Authority/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthority(int id, Authority authority)
        {
            if (id != authority.AuthorityId)
            {
                return BadRequest();
            }

            _context.Entry(authority).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authority
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Authority>> PostAuthority(Authority authority)
        {
            _context.Authorities.Add(authority);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthority", new { id = authority.AuthorityId }, authority);
        }

        // DELETE: api/Authority/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthority(int id)
        {
            var authority = await _context.Authorities.FindAsync(id);
            if (authority == null)
            {
                return NotFound();
            }

            _context.Authorities.Remove(authority);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Authority/5/Reports
        [HttpGet("{id}/Reports")]
        public async Task<ActionResult<IEnumerable<Report>>> GetAuthorityReports(int id)
        {
            return await _context.Reports
                .Where(e => e.ReportId == id)
                .ToListAsync();
        }

        // PUT: apit/Authority/5/Report/1
        // TODO: Need to improve security for verifying report and authority connection
        // TODO: Add ability to partially update reports
        [HttpPut("{id}/Reports/{reportId}")]
        public async Task<IActionResult> PutAuthorityReport(int id, int reportId, Report report)
        {
            if (id != report.AuthorityId)
            {
                return BadRequest();
            }
            if (report.Authority.AuthorityId != id) 
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!ReportExist(reportId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

        // DELETE: api/Authority/5/Reports/1
        // TODO: Need to improve security for verifying report and authority connection
        [HttpDelete("{id}/Reports/{reportId}")]
        public async Task<IActionResult> DeleteAuthorityReport(int id, int reportId)
        {
            var report = await _context.Reports.FindAsync(reportId);
            if (report == null)
            {
                return NotFound();
            }
            if (report.Authority.AuthorityId != id)
            {
                return BadRequest();
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorityExists(int id)
        {
            return _context.Authorities.Any(e => e.AuthorityId == id);
        }

        private bool ReportExist(int id)
        {
            return _context.Reports.Any(e => e.AuthorityId == id);
        }

    }
}
