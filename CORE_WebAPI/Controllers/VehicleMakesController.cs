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
    public class VehicleMakesController : ControllerBase
    {
        private readonly ProjectCALContext _context;

        public VehicleMakesController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/VehicleMakes
        [HttpGet]
        public IEnumerable<VehicleMake> GetVehicleMake()
        {
            return _context.VehicleMake;
        }

        // GET: api/VehicleMakes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleMake([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleMake = await _context.VehicleMake.FindAsync(id);

            if (vehicleMake == null)
            {
                return NotFound();
            }

            return Ok(vehicleMake);
        }

        // GET: api/Vehicles/descr/{descr}....
        [HttpGet("descr/{id}")]
        public async Task<IActionResult> GetMakeByDescr([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var make = await _context.VehicleMake.SingleOrDefaultAsync(m => m.VehicleMakeDescr == id);

            if (make == null)
            {
                return NotFound();
            }

            return Ok(make);
        }

        // PUT: api/VehicleMakes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleMake([FromRoute] int id, [FromBody] VehicleMake vehicleMake)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicleMake.VehicleMakeId)
            {
                return BadRequest();
            }

            _context.Entry(vehicleMake).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleMakeExists(id))
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

        // POST: api/VehicleMakes
        [HttpPost]
        public async Task<IActionResult> PostVehicleMake([FromBody] VehicleMake vehicleMake)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VehicleMake.Add(vehicleMake);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleMake", new { id = vehicleMake.VehicleMakeId }, vehicleMake);
        }

        // DELETE: api/VehicleMakes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleMake([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleMake = await _context.VehicleMake.FindAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            _context.VehicleMake.Remove(vehicleMake);
            await _context.SaveChangesAsync();

            return Ok(vehicleMake);
        }

        private bool VehicleMakeExists(int id)
        {
            return _context.VehicleMake.Any(e => e.VehicleMakeId == id);
        }
    }
}