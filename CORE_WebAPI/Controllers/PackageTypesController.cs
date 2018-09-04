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



        private readonly ProjectCALContext _context;

        public PackageTypesController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/PackageTypes
        [HttpGet]
        public IEnumerable<PackageType> GetPackageType()
        {
            return _context.PackageType.Include(price => price.PackageTypePrice);
        }

        // GET: /packagetypesgrid
        [HttpGet("/packagetypesgrid")]
        public PackageTypeGrid PackageTypeGrid()
        {
            PackageTypeGrid grid = new PackageTypeGrid();

            grid.totalCount = _context.PackageType.Include(price => price.PackageTypePrice).Count();

            grid.packageTypes = _context.PackageType.Include(price => price.PackageTypePrice);

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

            var packageType = await _context.PackageType.Include(price => price.PackageTypePrice)
                                                        .SingleOrDefaultAsync(m => m.PackageTypeId == id);

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
            PackageType updatePackageType = _context.PackageType.FirstOrDefault(c => c.PackageTypeId == id);

            updatePackageType.UpdateChangedFields(packageType);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != packageType.PackageTypeId)
            {
                return BadRequest();
            }

            _context.Entry(packageType).State = EntityState.Modified;

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PackageType.Add(packageType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackageType", new { id = packageType.PackageTypeId }, packageType);
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