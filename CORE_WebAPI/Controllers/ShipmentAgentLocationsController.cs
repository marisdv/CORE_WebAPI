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
    public class ShipmentAgentLocationsController : ControllerBase
    {
        private readonly ProjectCALServerContext _context;

        public ShipmentAgentLocationsController(ProjectCALServerContext context)
        {
            _context = context;
        }

        // GET: api/ShipmentAgentLocations
        [HttpGet]
        public IEnumerable<ShipmentAgentLocation> GetShipmentAgentLocation()
        {
            return _context.ShipmentAgentLocation;
        }

        // GET: api/ShipmentAgentLocations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipmentAgentLocation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipmentAgentLocation = await _context.ShipmentAgentLocation.FindAsync(id);

            if (shipmentAgentLocation == null)
            {
                return NotFound();
            }

            return Ok(shipmentAgentLocation);
        }

        // GET: api/ShipmentAgentLocations/agent/17
        [HttpGet("agent/{id}")]
        public async Task<IActionResult> GetLocationByAgent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipmentAgent = await _context.ShipmentAgentLocation
                                                .SingleOrDefaultAsync(m => m.AgentId == id);


            if (shipmentAgent == null)
            {
                return NotFound();
            }

            return Ok(shipmentAgent);
        }

        // PUT: api/ShipmentAgentLocations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipmentAgentLocation([FromRoute] int id, [FromBody] ShipmentAgentLocation shipmentAgentLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shipmentAgentLocation.CurrentLocId)
            {
                return BadRequest();
            }

            _context.Entry(shipmentAgentLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipmentAgentLocationExists(id))
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

        // POST: api/ShipmentAgentLocations
        [HttpPost]
        public async Task<IActionResult> PostShipmentAgentLocation([FromBody] ShipmentAgentLocation shipmentAgentLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ShipmentAgentLocation.Add(shipmentAgentLocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShipmentAgentLocation", new { id = shipmentAgentLocation.CurrentLocId }, shipmentAgentLocation);
        }

        // DELETE: api/ShipmentAgentLocations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipmentAgentLocation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipmentAgentLocation = await _context.ShipmentAgentLocation.FindAsync(id);
            if (shipmentAgentLocation == null)
            {
                return NotFound();
            }

            _context.ShipmentAgentLocation.Remove(shipmentAgentLocation);
            await _context.SaveChangesAsync();

            return Ok(shipmentAgentLocation);
        }

        private bool ShipmentAgentLocationExists(int id)
        {
            return _context.ShipmentAgentLocation.Any(e => e.CurrentLocId == id);
        }
    }
}