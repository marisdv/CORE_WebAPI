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
    public class VehiclePacakageLinesController : ControllerBase
    {
        private readonly ProjectCALContext _context;

        public VehiclePacakageLinesController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/VehiclePacakageLines
        [HttpGet]
        public IEnumerable<VehiclePacakageLine> GetVehiclePacakageLine()
        {
            return _context.VehiclePacakageLine;
        }

        // GET: api/VehiclePacakageLines/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehiclePacakageLine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehiclePacakageLine = await _context.VehiclePacakageLine.FindAsync(id);

            if (vehiclePacakageLine == null)
            {
                return NotFound();
            }

            return Ok(vehiclePacakageLine);
        }

        // PUT: api/VehiclePacakageLines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiclePacakageLine([FromRoute] int id, [FromBody] VehiclePacakageLine vehiclePacakageLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehiclePacakageLine.VehicleTypeId)
            {
                return BadRequest();
            }

            _context.Entry(vehiclePacakageLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiclePacakageLineExists(id))
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

        // POST: api/VehiclePacakageLines
        [HttpPost]
        public async Task<IActionResult> PostVehiclePacakageLine([FromBody] VehiclePacakageLine vehiclePacakageLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VehiclePacakageLine.Add(vehiclePacakageLine);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VehiclePacakageLineExists(vehiclePacakageLine.VehicleTypeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVehiclePacakageLine", new { id = vehiclePacakageLine.VehicleTypeId }, vehiclePacakageLine);
        }

        // DELETE: api/VehiclePacakageLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiclePacakageLine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehiclePacakageLine = await _context.VehiclePacakageLine.FindAsync(id);
            if (vehiclePacakageLine == null)
            {
                return NotFound();
            }

            _context.VehiclePacakageLine.Remove(vehiclePacakageLine);
            await _context.SaveChangesAsync();

            return Ok(vehiclePacakageLine);
        }

        private bool VehiclePacakageLineExists(int id)
        {
            return _context.VehiclePacakageLine.Any(e => e.VehicleTypeId == id);
        }
    }
}