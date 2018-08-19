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
    [Route("api/ShipmentAddresses")]
    [EnableCors("MyPolicy")]
    public class ShipmentAddressesController : Controller
    {
        private readonly ProjectCALContext _context;

        public ShipmentAddressesController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/ShipmentAddresses
        [HttpGet]
        public IEnumerable<ShipmentAddress> GetShipmentAddress()
        {
            return _context.ShipmentAddress;
        }

        // GET: api/ShipmentAddresses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipmentAddress([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipmentAddress = await _context.ShipmentAddress.SingleOrDefaultAsync(m => m.AddressId == id);

            if (shipmentAddress == null)
            {
                return NotFound();
            }

            return Ok(shipmentAddress);
        }

        // PUT: api/ShipmentAddresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipmentAddress([FromRoute] int id, [FromBody] ShipmentAddress shipmentAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shipmentAddress.AddressId)
            {
                return BadRequest();
            }

            _context.Entry(shipmentAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipmentAddressExists(id))
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

        // POST: api/ShipmentAddresses
        [HttpPost]
        public async Task<IActionResult> PostShipmentAddress([FromBody] ShipmentAddress shipmentAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ShipmentAddress.Add(shipmentAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShipmentAddress", new { id = shipmentAddress.AddressId }, shipmentAddress);
        }

        // DELETE: api/ShipmentAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipmentAddress([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipmentAddress = await _context.ShipmentAddress.SingleOrDefaultAsync(m => m.AddressId == id);
            if (shipmentAddress == null)
            {
                return NotFound();
            }

            _context.ShipmentAddress.Remove(shipmentAddress);
            await _context.SaveChangesAsync();

            return Ok(shipmentAddress);
        }

        private bool ShipmentAddressExists(int id)
        {
            return _context.ShipmentAddress.Any(e => e.AddressId == id);
        }
    }
}