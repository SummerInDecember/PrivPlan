using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using privplan_api.Models;
using privplan_api.Wrappers;

namespace privplan_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllowedAccountsController : ControllerBase
    {
        private readonly PrivPlanContext _context;

        public AllowedAccountsController(PrivPlanContext context)
        {
            _context = context;
        }

        // GET: api/AllowedAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllowedAccounts>>> GetAllowedDevices()
        {
            return await _context.AllowedAccounts.ToListAsync();
        }

        // GET: api/AllowedAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AllowedAccounts>> GetAllowedAccounts(int id)
        {
            var allowedAccounts = await _context.AllowedAccounts.FindAsync(id);

            if (allowedAccounts == null)
            {
                return NotFound();
            }

            return allowedAccounts;
        }

        // PUT: api/AllowedAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAllowedAccounts(int id, AllowedAccounts allowedAccounts)
        {
            if (id != allowedAccounts.Id)
            {
                return BadRequest();
            }

            _context.Entry(allowedAccounts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllowedAccountsExists(id))
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

        // POST: api/AllowedAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AllowedAccounts>> PostAllowedAccounts(AllowedAccountsWrapper allowedAccountsWrapper)
        {
            // TODO: Implement authentication
            _context.AllowedAccounts.Add(allowedAccountsWrapper.Request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllowedAccounts", new { id = allowedAccountsWrapper.Request.Id }, allowedAccountsWrapper.Request);
        }

        // DELETE: api/AllowedAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAllowedAccounts(int id)
        {
            var allowedAccounts = await _context.AllowedAccounts.FindAsync(id);
            if (allowedAccounts == null)
            {
                return NotFound();
            }

            _context.AllowedAccounts.Remove(allowedAccounts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AllowedAccountsExists(int id)
        {
            return _context.AllowedAccounts.Any(e => e.Id == id);
        }
    }
}
