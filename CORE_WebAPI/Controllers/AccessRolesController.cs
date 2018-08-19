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
    [Route("api/AccessRoles")]
    [EnableCors("MyPolicy")]
    public class AccessRolesController : Controller
    {
        private readonly ProjectCALContext _context;

        public AccessRolesController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/AccessRoles
        [HttpGet]
        public IEnumerable<AccessRole> GetAccessRole()
        {
            return _context.AccessRole;
        }

        // GET: api/AccessRoles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccessRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accessRole = await _context.AccessRole.SingleOrDefaultAsync(m => m.AccessRoleId == id);

            if (accessRole == null)
            {
                return NotFound();
            }

            return Ok(accessRole);
        }

        // PUT: api/AccessRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessRole([FromRoute] int id, [FromBody] AccessRole accessRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accessRole.AccessRoleId)
            {
                return BadRequest();
            }

            _context.Entry(accessRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessRoleExists(id))
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

        // POST: api/AccessRoles
        [HttpPost]
        public async Task<IActionResult> PostAccessRole([FromBody] AccessRole accessRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AccessRole.Add(accessRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccessRole", new { id = accessRole.AccessRoleId }, accessRole);
        }

        // DELETE: api/AccessRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accessRole = await _context.AccessRole.SingleOrDefaultAsync(m => m.AccessRoleId == id);
            if (accessRole == null)
            {
                return NotFound();
            }

            _context.AccessRole.Remove(accessRole);
            await _context.SaveChangesAsync();

            return Ok(accessRole);
        }

        private bool AccessRoleExists(int id)
        {
            return _context.AccessRole.Any(e => e.AccessRoleId == id);
        }
    }
}