using Microsoft.AspNetCore.Mvc;
using HawkManagement.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HawkManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employer>>> GetEmployers()
        {
            return await _context.Employers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employer>> GetEmployer(int id)
        {
            var employer = await _context.Employers.FindAsync(id);
            if (employer == null)
            {
                return NotFound();
            }
            return employer;
        }

        [HttpPost]
        public async Task<ActionResult<Employer>> PostEmployer(Employer employer)
        {
            _context.Employers.Add(employer);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEmployer", new { id = employer.EmployerID }, employer);
        }
    }
}
