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
    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("MyPolicy")]
    public class AccessRoleAreasController : ControllerBase
    {
        private readonly ProjectCALContext _context;

        public AccessRoleAreasController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/AccessRoleAreas
        [HttpGet]
        public IEnumerable<AccessRoleArea> GetAccessRoleArea()
        {
            return _context.AccessRoleArea;
        }

        // GET: api/AccessRoleAreas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccessRoleArea([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accessRoleArea = await _context.AccessRoleArea.FindAsync(id);

            if (accessRoleArea == null)
            {
                return NotFound();
            }

            return Ok(accessRoleArea);
        }

        // PUT: api/AccessRoleAreas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessRoleArea([FromRoute] int id, [FromBody] AccessRoleArea accessRoleArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accessRoleArea.AccessRoleId)
            {
                return BadRequest();
            }

            _context.Entry(accessRoleArea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessRoleAreaExists(id))
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

        // POST: api/AccessRoleAreas
        [HttpPost]
        public async Task<IActionResult> PostAccessRoleArea([FromBody] AccessRoleArea accessRoleArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AccessRoleArea.Add(accessRoleArea);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccessRoleAreaExists(accessRoleArea.AccessRoleId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccessRoleArea", new { id = accessRoleArea.AccessRoleId }, accessRoleArea);
        }

        // DELETE: api/AccessRoleAreas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessRoleArea([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accessRoleArea = await _context.AccessRoleArea.FindAsync(id);
            if (accessRoleArea == null)
            {
                return NotFound();
            }

            _context.AccessRoleArea.Remove(accessRoleArea);
            await _context.SaveChangesAsync();

            return Ok(accessRoleArea);
        }

        private bool AccessRoleAreaExists(int id)
        {
            return _context.AccessRoleArea.Any(e => e.AccessRoleId == id);
        }
    }
}