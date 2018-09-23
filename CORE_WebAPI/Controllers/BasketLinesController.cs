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
    public class BasketLinesController : ControllerBase
    {
        private readonly ProjectCALContext _context;

        public BasketLinesController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/BasketLines
        [HttpGet]
        public IEnumerable<BasketLine> GetBasketLine()
        {
            return _context.BasketLine;
        }

        // GET: api/BasketLines/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBasketLine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var basketLine = _context.BasketLine.Where(line => line.SenderId == id);

            if (basketLine == null)
            {
                return NotFound();
            }

            return Ok(basketLine);
        }

        // PUT: api/BasketLines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBasketLine([FromRoute] int id, [FromBody] BasketLine basketLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != basketLine.SenderId)
            {
                return BadRequest();
            }

            _context.Entry(basketLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BasketLineExists(id))
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

        // POST: api/BasketLines
        [HttpPost]
        public async Task<IActionResult> PostBasketLine([FromBody] BasketLine basketLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BasketLine.Add(basketLine);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BasketLineExists(basketLine.SenderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBasketLine", new { id = basketLine.SenderId }, basketLine);
        }

        // DELETE: api/BasketLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasketLine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var basketLine = await _context.BasketLine.FindAsync(id);
            if (basketLine == null)
            {
                return NotFound();
            }

            _context.BasketLine.Remove(basketLine);
            await _context.SaveChangesAsync();

            return Ok(basketLine);
        }

        private bool BasketLineExists(int id)
        {
            return _context.BasketLine.Any(e => e.SenderId == id);
        }
    }
}