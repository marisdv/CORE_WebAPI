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
    [Route("api/FixedPrices")]
    //[ApiController]
    [EnableCors("MyPolicy")]
    public class FixedPricesController : ControllerBase
    {
        private readonly ProjectCALServerContext _context;

        public FixedPricesController(ProjectCALServerContext context)
        {
            _context = context;
        }

        // GET: api/FixedPrices
        [HttpGet]
        public IEnumerable<FixedPrice> GetFixedPrice()
        {
            return _context.FixedPrice;
        }

        // GET: /fixedpricesgrid
        [HttpGet("/fixedpricesgrid")]
        public FixedPriceGrid FixedPriceGrid()
        {
            FixedPriceGrid grid = new FixedPriceGrid();

            grid.totalCount = _context.FixedPrice.Count();

            grid.fixedPrices = _context.FixedPrice;

            return grid;
        }

        // GET: api/FixedPrices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFixedPrice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fixedPrice = await _context.FixedPrice.FindAsync(id);

            if (fixedPrice == null)
            {
                return NotFound();
            }

            return Ok(fixedPrice);
        }

        // PUT: api/FixedPrices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFixedPrice([FromRoute] int id, [FromBody] FixedPrice fixedPrice)
        {

            FixedPrice updateFixedPrice = _context.FixedPrice.FirstOrDefault(f => f.FixedPriceId == id);

            updateFixedPrice.UpdateChangedFields(fixedPrice);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fixedPrice.FixedPriceId)
            {
                return BadRequest();
            }

            _context.Entry(fixedPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FixedPriceExists(id))
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

        // POST: api/FixedPrices
        [HttpPost]
        public async Task<IActionResult> PostFixedPrice([FromBody] FixedPrice fixedPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FixedPrice.Add(fixedPrice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFixedPrice", new { id = fixedPrice.FixedPriceId }, fixedPrice);
        }

        // DELETE: api/FixedPrices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFixedPrice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fixedPrice = await _context.FixedPrice.FindAsync(id);
            if (fixedPrice == null)
            {
                return NotFound();
            }

            _context.FixedPrice.Remove(fixedPrice);
            await _context.SaveChangesAsync();

            return Ok(fixedPrice);
        }

        private bool FixedPriceExists(int id)
        {
            return _context.FixedPrice.Any(e => e.FixedPriceId == id);
        }
    }
}