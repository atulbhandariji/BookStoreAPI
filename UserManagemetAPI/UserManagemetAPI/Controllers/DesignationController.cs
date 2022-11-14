using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagemetAPI.Model;

namespace UserManagemetAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public DesignationController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/Designation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TBLDesignation>>> GetTBLDesignations()
        {
            return await _context.TBLDesignations.ToListAsync();
        }

        // GET: api/Designation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TBLDesignation>> GetTBLDesignation(int id)
        {
            var tBLDesignation = await _context.TBLDesignations.FindAsync(id);

            if (tBLDesignation == null)
            {
                return NotFound();
            }

            return tBLDesignation;
        }

        // PUT: api/Designation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTBLDesignation(int id, TBLDesignation tBLDesignation)
        {
            if (id != tBLDesignation.ID)
            {
                return BadRequest();
            }

            _context.Entry(tBLDesignation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TBLDesignationExists(id))
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

        // POST: api/Designation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TBLDesignation>> PostTBLDesignation(TBLDesignation tBLDesignation)
        {
            _context.TBLDesignations.Add(tBLDesignation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTBLDesignation", new { id = tBLDesignation.ID }, tBLDesignation);
        }

        // DELETE: api/Designation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTBLDesignation(int id)
        {
            var tBLDesignation = await _context.TBLDesignations.FindAsync(id);
            if (tBLDesignation == null)
            {
                return NotFound();
            }

            _context.TBLDesignations.Remove(tBLDesignation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TBLDesignationExists(int id)
        {
            return _context.TBLDesignations.Any(e => e.ID == id);
        }
    }
}
