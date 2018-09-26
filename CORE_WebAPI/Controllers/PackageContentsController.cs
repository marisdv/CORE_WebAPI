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
    public class PackageContentsController : ControllerBase
    {
        private readonly ProjectCALServerContext _context;

        public PackageContentsController(ProjectCALServerContext context)
        {
            _context = context;
        }

        // GET: api/PackageContents
        [HttpGet]
        public IEnumerable<PackageContent> GetPackageContent()
        {
            return _context.PackageContent;
        }

        // GET: api/PackageContents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageContent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageContent = await _context.PackageContent.FindAsync(id);

            if (packageContent == null)
            {
                return NotFound();
            }

            return Ok(packageContent);
        }

        // PUT: api/PackageContents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackageContent([FromRoute] int id, [FromBody] PackageContent packageContent)
        {
            PackageContent updatePackageContent = _context.PackageContent.FirstOrDefault(p => p.PackageContentId == id);

            updatePackageContent.UpdateChangedFields(packageContent);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatePackageContent.PackageContentId)
            {
                return BadRequest();
            }

            _context.Entry(updatePackageContent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageContentExists(id))
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

        // POST: api/PackageContents
        [HttpPost]
        public async Task<IActionResult> PostPackageContent([FromBody] PackageContent packageContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PackageContent.Add(packageContent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackageContent", new { id = packageContent.PackageContentId }, packageContent);
        }

        // DELETE: api/PackageContents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackageContent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageContent = await _context.PackageContent.FindAsync(id);
            if (packageContent == null)
            {
                return NotFound();
            }

            _context.PackageContent.Remove(packageContent);
            await _context.SaveChangesAsync();

            return Ok(packageContent);
        }

        private bool PackageContentExists(int id)
        {
            return _context.PackageContent.Any(e => e.PackageContentId == id);
        }
    }
}