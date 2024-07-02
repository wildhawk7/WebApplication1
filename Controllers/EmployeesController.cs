using Microsoft.AspNetCore.Mvc;
using HawkManagement.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HawkManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(AppDbContext context, ILogger<EmployeesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            try
            {
                employee.Password = BCrypt.Net.BCrypt.HashPassword(employee.Password);  // Hash the password before saving
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetEmployee", new { id = employee.EmployeeID }, employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while creating employee: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
