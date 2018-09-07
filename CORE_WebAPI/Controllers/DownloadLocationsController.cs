using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CORE_WebAPI.Models;

namespace CORE_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadLocationsController : ControllerBase
    {
        private readonly ProjectCALContext _context;

        public DownloadLocationsController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/DownloadLocations
        [HttpGet]
        public IEnumerable<DownloadLocation> GetDownloadLocation()
        {
            return _context.DownloadLocation;
        }

        // GET: api/DownloadLocations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDownloadLocation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var downloadLocation = await _context.DownloadLocation.FindAsync(id);

            if (downloadLocation == null)
            {
                return NotFound();
            }

            return Ok(downloadLocation);
        }

        // PUT: api/DownloadLocations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDownloadLocation([FromRoute] int id, [FromBody] DownloadLocation downloadLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != downloadLocation.DownloadId)
            {
                return BadRequest();
            }

            _context.Entry(downloadLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DownloadLocationExists(id))
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

        // POST: api/DownloadLocations
        [HttpPost]
        public async Task<IActionResult> PostDownloadLocation([FromBody] DownloadLocation downloadLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DownloadLocation.Add(downloadLocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDownloadLocation", new { id = downloadLocation.DownloadId }, downloadLocation);
        }

        // DELETE: api/DownloadLocations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDownloadLocation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var downloadLocation = await _context.DownloadLocation.FindAsync(id);
            if (downloadLocation == null)
            {
                return NotFound();
            }

            _context.DownloadLocation.Remove(downloadLocation);
            await _context.SaveChangesAsync();

            return Ok(downloadLocation);
        }

        private bool DownloadLocationExists(int id)
        {
            return _context.DownloadLocation.Any(e => e.DownloadId == id);
        }
    }
}