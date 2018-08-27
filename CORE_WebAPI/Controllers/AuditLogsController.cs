using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CORE_WebAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace CORE_WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/AuditLogs")]
    [EnableCors("MyPolicy")]
    public class AuditLogsController : Controller
    {
        private readonly ProjectCALContext _context;

        public AuditLogsController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/AuditLogs
        [HttpGet]
        public IEnumerable<AuditLog> GetAuditLog()
        {
            //return _context.AuditLog;
            
            return _context.AuditLog.Include(auditLog => auditLog.AuditType);
        }

        // GET: /auditloggrid
        [HttpGet("/auditloggrid")]
        public AuditLogGrid AuditLogGrid()
        {
            AuditLogGrid grid = new AuditLogGrid();

            grid.totalCount = _context.AuditLog.Include(auditLog => auditLog.AuditType).Count();

            grid.auditLogs = _context.AuditLog.Include(auditLog => auditLog.AuditType);

            return grid;
        }

        // GET: api/AuditLogs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuditLog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var auditLog = await _context.AuditLog.Include(audit => audit.AuditType)
                                                  .SingleOrDefaultAsync(m => m.AuditId == id);

            if (auditLog == null)
            {
                return NotFound();
            }

            return Ok(auditLog);
        }

        // PUT: api/AuditLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuditLog([FromRoute] int id, [FromBody] AuditLog auditLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != auditLog.AuditId)
            {
                return BadRequest();
            }

            _context.Entry(auditLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditLogExists(id))
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

        // POST: api/AuditLogs
        [HttpPost]
        public async Task<IActionResult> PostAuditLog([FromBody] AuditLog auditLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AuditLog.Add(auditLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuditLog", new { id = auditLog.AuditId }, auditLog);
        }

        // DELETE: api/AuditLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuditLog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var auditLog = await _context.AuditLog.SingleOrDefaultAsync(m => m.AuditId == id);
            if (auditLog == null)
            {
                return NotFound();
            }

            _context.AuditLog.Remove(auditLog);
            await _context.SaveChangesAsync();

            return Ok(auditLog);
        }

        private bool AuditLogExists(int id)
        {
            return _context.AuditLog.Any(e => e.AuditId == id);
        }
    }
}