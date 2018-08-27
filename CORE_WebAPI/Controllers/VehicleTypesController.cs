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
    [Route("api/VehicleTypes")]
    [EnableCors("MyPolicy")]
    public class VehicleTypesController : Controller
    {
        private readonly ProjectCALContext _context;

        public VehicleTypesController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/VehicleTypes
        [HttpGet]
        public IEnumerable<VehicleType> GetVehicleType()
        {
            return _context.VehicleType;
        }

        // GET: /vehicletypesgrid
        [HttpGet("/vehicletypesgrid")]
        public VehicleTypeGrid VehicleTypeGrid()
        {
            VehicleTypeGrid grid = new VehicleTypeGrid();

            grid.totalCount = _context.VehicleType.Count();

            grid.vehicleTypes= _context.VehicleType;

            return grid;
        }

        // GET: api/VehicleTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleType = await _context.VehicleType.SingleOrDefaultAsync(m => m.VehicleTypeId == id);

            if (vehicleType == null)
            {
                return NotFound();
            }

            return Ok(vehicleType);
        }

        // PUT: api/VehicleTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleType([FromRoute] int id, [FromBody] VehicleType vehicleType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicleType.VehicleTypeId)
            {
                return BadRequest();
            }

            _context.Entry(vehicleType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleTypeExists(id))
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

        // POST: api/VehicleTypes
        [HttpPost]
        public async Task<IActionResult> PostVehicleType([FromBody] VehicleType vehicleType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VehicleType.Add(vehicleType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleType", new { id = vehicleType.VehicleTypeId }, vehicleType);
        }

        // DELETE: api/VehicleTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleType = await _context.VehicleType.SingleOrDefaultAsync(m => m.VehicleTypeId == id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            _context.VehicleType.Remove(vehicleType);
            await _context.SaveChangesAsync();

            return Ok(vehicleType);
        }

        private bool VehicleTypeExists(int id)
        {
            return _context.VehicleType.Any(e => e.VehicleTypeId == id);
        }
    }
}