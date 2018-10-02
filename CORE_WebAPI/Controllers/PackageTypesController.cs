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
    [Route("api/PackageTypes")]
    [EnableCors("MyPolicy")]
    public class PackageTypesController : Controller
    {
        private readonly ProjectCALServerContext _context;
        private string baseURL = "C:\\img\\packages\\";

        public PackageTypesController(ProjectCALServerContext context)
        {
            _context = context;
        }

        // GET: api/PackageTypes
        [HttpGet]
        public IEnumerable<PackageType> GetPackageType()
        {
            List<PackageType> packageType = _context.PackageType.ToList();
            packageType.ForEach(x=> x.PackageTypeImage = null);
            return packageType;
        }

        // GET: /api/package/image/4
        [HttpGet("image/{id}")]
        public IActionResult GetPackageImage(int id)
        {
            byte[] imageByte = System.IO.File.ReadAllBytes(baseURL + id + ".jpg");
            return File(imageByte, "image/jpeg");
        }

        // GET: /packagetypesgrid
        [HttpGet("/packagetypesgrid")]
        public PackageTypeGrid PackageTypeGrid()
        {
            PackageTypeGrid grid = new PackageTypeGrid();

            grid.totalCount = _context.PackageType.Count();

            grid.packageTypes = _context.PackageType;

            return grid;
        }

        // GET: api/PackageTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageType = await _context.PackageType.SingleOrDefaultAsync(m => m.PackageTypeId == id);
            packageType.PackageTypeImage = null;
            if (packageType == null)
            {
                return NotFound();
            }

            return Ok(packageType);
        }

        // PUT: api/PackageTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackageType([FromRoute] int id, [FromBody] PackageType packageType)
        {
            System.Diagnostics.Debugger.Break();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != packageType.PackageTypeId)
            {
                return BadRequest();
            }

            PackageType updatePackageType = _context.PackageType.FirstOrDefault(p => p.PackageTypeId == id);

            updatePackageType.UpdateChangedFields(packageType);

            _context.Entry(updatePackageType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageTypeExists(id))
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

        // POST: api/PackageTypes
        [HttpPost]
        public async Task<IActionResult> PostPackageType([FromBody] PackageType packageType)
        {
            try
            {
                //System.Diagnostics.Debugger.Break();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.PackageType.Add(packageType);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPackageType", new { id = packageType.PackageTypeId }, packageType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            
        }

        // DELETE: api/PackageTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackageType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageType = await _context.PackageType.SingleOrDefaultAsync(m => m.PackageTypeId == id);
            if (packageType == null)
            {
                return NotFound();
            }

            _context.PackageType.Remove(packageType);
            await _context.SaveChangesAsync();

            return Ok(packageType);
        }

        private bool PackageTypeExists(int id)
        {
            return _context.PackageType.Any(e => e.PackageTypeId == id);
        }
    }
}