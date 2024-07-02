using Microsoft.AspNetCore.Mvc;
using HawkManagement.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HawkManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginLogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginLogsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginLog>>> GetLoginLogs()
        {
            return await _context.LoginLogs.Include(l => l.Employee).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoginLog>> GetLoginLog(int id)
        {
            var loginLog = await _context.LoginLogs.Include(l => l.Employee).FirstOrDefaultAsync(l => l.LogID == id);
            if (loginLog == null)
            {
                return NotFound();
            }
            return loginLog;
        }

        [HttpPost]
        public async Task<ActionResult<LoginLog>> PostLoginLog(LoginLog loginLog)
        {
            _context.LoginLogs.Add(loginLog);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetLoginLog", new { id = loginLog.LogID }, loginLog);
        }
    }
}
