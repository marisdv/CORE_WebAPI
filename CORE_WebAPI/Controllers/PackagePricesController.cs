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
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class PackagePricesController : ControllerBase
    {
        private readonly ProjectCALContext _context;

        public PackagePricesController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/PackagePrices
        [HttpGet]
        public IEnumerable<PackagePrice> GetPackagePrice()
        {
            return _context.PackagePrice.Include(type => type.PackageType);
        }

        // GET: /packagepricesgrid
        [HttpGet("/packagetypepricesgrid")]
        public PackagePriceGrid PackagePriceGrid()
        {
            PackagePriceGrid grid = new PackagePriceGrid();

            grid.totalCount = _context.PackagePrice.Count();

            grid.packageTypePrices = _context.PackagePrice;

            return grid;
        }

        // GET: api/PackagePrices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackagePrice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packagePrice = await _context.PackagePrice.Include(type => type.PackageType).SingleOrDefaultAsync(packageprice => packageprice.PackagePriceId == id);

            if (packagePrice == null)
            {
                return NotFound();
            }

            return Ok(packagePrice);
        }

        // PUT: api/PackagePrices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackagePrice([FromRoute] int id, [FromBody] PackagePrice packagePrice)
        {

            PackagePrice updatePackagePrice = _context.PackagePrice.FirstOrDefault(p => p.PackagePriceId == id);

            updatePackagePrice.UpdateChangedFields(packagePrice);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatePackagePrice.PackagePriceId)
            {
                return BadRequest();
            }

            _context.Entry(updatePackagePrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackagePriceExists(id))
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

        // POST: api/PackagePrices
        [HttpPost]
        public async Task<IActionResult> PostPackagePrice([FromBody] PackagePrice packagePrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PackagePrice.Add(packagePrice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackagePrice", new { id = packagePrice.PackagePriceId }, packagePrice);
        }

        // DELETE: api/PackagePrices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackagePrice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packagePrice = await _context.PackagePrice.FindAsync(id);
            if (packagePrice == null)
            {
                return NotFound();
            }

            _context.PackagePrice.Remove(packagePrice);
            await _context.SaveChangesAsync();

            return Ok(packagePrice);
        }

        private bool PackagePriceExists(int id)
        {
            return _context.PackagePrice.Any(e => e.PackagePriceId == id);
        }
    }
}