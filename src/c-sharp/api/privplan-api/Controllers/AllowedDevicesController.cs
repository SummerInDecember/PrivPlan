using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using privplan_api.Models;


namespace privplan_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllowedDevicesController : ControllerBase
    {
        private readonly PrivPlanContext _context;

        public AllowedDevicesController(PrivPlanContext context)
        {
            _context = context;
        }

        // GET: api/AllowedDevices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllowedDevices>>> GetAllowedDevices()
        {
            return await _context.AllowedDevices.ToListAsync();
        }

        // GET: api/AllowedDevices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AllowedDevices>> GetAllowedDevices(int id)
        {
            var allowedDevices = await _context.AllowedDevices.FindAsync(id);

            if (allowedDevices == null)
            {
                return NotFound();
            }

            return allowedDevices;
        }

        // PUT: api/AllowedDevices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAllowedDevices(int id, AllowedDevices allowedDevices)
        {
            if (id != allowedDevices.Id)
            {
                return BadRequest();
            }

            _context.Entry(allowedDevices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllowedDevicesExists(id))
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

        // POST: api/AllowedDevices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AllowedDevices>> PostAllowedDevices(AllowedDevices allowedDevices)
        {
            _context.AllowedDevices.Add(allowedDevices);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllowedDevices", new { id = allowedDevices.Id }, allowedDevices);
        }

        // DELETE: api/AllowedDevices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAllowedDevices(int id)
        {
            var allowedDevices = await _context.AllowedDevices.FindAsync(id);
            if (allowedDevices == null)
            {
                return NotFound();
            }

            _context.AllowedDevices.Remove(allowedDevices);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AllowedDevicesExists(int id)
        {
            return _context.AllowedDevices.Any(e => e.Id == id);
        }
    }
}
