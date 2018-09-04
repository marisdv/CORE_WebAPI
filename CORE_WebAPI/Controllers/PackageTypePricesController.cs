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
    [Route("api/PackageTypePrices")]
    [EnableCors("MyPolicy")]
    public class PackageTypePricesController : Controller
    {
        private readonly ProjectCALContext _context;

        public PackageTypePricesController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/PackageTypePrices
        [HttpGet]
        public IEnumerable<PackageTypePrice> GetPackageTypePrice()
        {
            return _context.PackageTypePrice;
        }

        // GET: /packagetypepricesgrid
        [HttpGet("/packagetypepricesgrid")]
        public PackageTypePriceGrid PackageTypePriceGrid()
        {
            PackageTypePriceGrid grid = new PackageTypePriceGrid();

            grid.totalCount = _context.PackageTypePrice.Count();

            grid.packageTypePrices = _context.PackageTypePrice;

            return grid;
        }

        // GET: api/PackageTypePrices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageTypePrice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageTypePrice = await _context.PackageTypePrice.SingleOrDefaultAsync(m => m.PackagePriceId == id);

            if (packageTypePrice == null)
            {
                return NotFound();
            }

            return Ok(packageTypePrice);
        }

        // PUT: api/PackageTypePrices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackageTypePrice([FromRoute] int id, [FromBody] PackageTypePrice packageTypePrice)
        {

            //this has a composite key - PK/FK because it's an associative
            //PackageTypePrice updatePackageTypePrice = _context.PackageTypePrice.FirstOrDefault(c => c.PackageTypePriceId == id);

            //updatePackageTypePrice.UpdateChangedFields(packageTypePrice);

            //use updatePackageTypePrice from here on ???

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != packageTypePrice.PackagePriceId)
            {
                return BadRequest();
            }

            _context.Entry(packageTypePrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageTypePriceExists(id))
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

        // POST: api/PackageTypePrices
        [HttpPost]
        public async Task<IActionResult> PostPackageTypePrice([FromBody] PackageTypePrice packageTypePrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PackageTypePrice.Add(packageTypePrice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackageTypePrice", new { id = packageTypePrice.PackagePriceId }, packageTypePrice);
        }

        // DELETE: api/PackageTypePrices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackageTypePrice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageTypePrice = await _context.PackageTypePrice.SingleOrDefaultAsync(m => m.PackagePriceId == id);
            if (packageTypePrice == null)
            {
                return NotFound();
            }

            _context.PackageTypePrice.Remove(packageTypePrice);
            await _context.SaveChangesAsync();

            return Ok(packageTypePrice);
        }

        private bool PackageTypePriceExists(int id)
        {
            return _context.PackageTypePrice.Any(e => e.PackagePriceId == id);
        }
    }
}