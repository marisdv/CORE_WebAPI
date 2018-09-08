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
    [ApiController]
    [Produces("application/json")]
    [EnableCors("MyPolicy")]
    public class AccessAreasController : ControllerBase
    {
        private readonly ProjectCALContext _context;

        public AccessAreasController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/AccessAreas
        [HttpGet]
        public IEnumerable<AccessArea> GetAccessArea()
        {
            return _context.AccessArea;
        }

        // GET: api/AccessAreas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccessArea([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accessArea = await _context.AccessArea.FindAsync(id);

            if (accessArea == null)
            {
                return NotFound();
            }

            return Ok(accessArea);
        }

        // PUT: api/AccessAreas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessArea([FromRoute] int id, [FromBody] AccessArea accessArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accessArea.AccessAreaId)
            {
                return BadRequest();
            }

            _context.Entry(accessArea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessAreaExists(id))
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

        // POST: api/AccessAreas
        [HttpPost]
        public async Task<IActionResult> PostAccessArea([FromBody] AccessArea accessArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AccessArea.Add(accessArea);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccessArea", new { id = accessArea.AccessAreaId }, accessArea);
        }

        // DELETE: api/AccessAreas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessArea([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accessArea = await _context.AccessArea.FindAsync(id);
            if (accessArea == null)
            {
                return NotFound();
            }

            _context.AccessArea.Remove(accessArea);
            await _context.SaveChangesAsync();

            return Ok(accessArea);
        }

        private bool AccessAreaExists(int id)
        {
            return _context.AccessArea.Any(e => e.AccessAreaId == id);
        }
    }
}