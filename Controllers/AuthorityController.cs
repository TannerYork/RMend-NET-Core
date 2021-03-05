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
        private readonly AuthorityContext _context;

        public AuthorityController(AuthorityContext context)
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
        public async Task<ActionResult<Authority>> GetAuthority(long id)
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
        public async Task<IActionResult> PutAuthority(long id, Authority authority)
        {
            if (id != authority.Id)
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

            return CreatedAtAction("GetAuthority", new { id = authority.Id }, authority);
        }

        // DELETE: api/Authority/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthority(long id)
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

        private bool AuthorityExists(long id)
        {
            return _context.Authorities.Any(e => e.Id == id);
        }

    }
}
