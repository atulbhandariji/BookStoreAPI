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
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TBLEmployee>>> GetTBLEmployees()
        {
            //   return await _context.TBLEmployees.ToListAsync();

            var employees=(from e in _context.TBLEmployees
             join d in _context.TBLDesignations
             on e.DesignationID equals d.ID
             select new TBLEmployee
             {
                 Id = e.Id,
                 Name = e.Name,
                 LastName = e.LastName,
                 Email = e.Email,
                 Age = e.Age,
                 DesignationID = e.DesignationID,
                 Designation = d.Designation,
                 Doj = e.Doj,
                 Gender = e.Gender,
                 IsActive = e.IsActive,
                 IsMarried = e.IsMarried
             }
            ).ToListAsync();
          return await employees;
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TBLEmployee>> GetTBLEmployee(int id)
        {
            var tBLEmployee = await _context.TBLEmployees.FindAsync(id);

            if (tBLEmployee == null)
            {
                return NotFound();
            }

            return tBLEmployee;
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTBLEmployee(int id, TBLEmployee tBLEmployee)
        {
            if (id != tBLEmployee.Id)
            {
                return BadRequest();
            }

            _context.Entry(tBLEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TBLEmployeeExists(id))
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

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TBLEmployee>> PostTBLEmployee(TBLEmployee tBLEmployee)
        {
            _context.TBLEmployees.Add(tBLEmployee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTBLEmployee", new { id = tBLEmployee.Id }, tBLEmployee);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTBLEmployee(int id)
        {
            var tBLEmployee = await _context.TBLEmployees.FindAsync(id);
            if (tBLEmployee == null)
            {
                return NotFound();
            }

            _context.TBLEmployees.Remove(tBLEmployee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TBLEmployeeExists(int id)
        {
            return _context.TBLEmployees.Any(e => e.Id == id);
        }
    }
}
