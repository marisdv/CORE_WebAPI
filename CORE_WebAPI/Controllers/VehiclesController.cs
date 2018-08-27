using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CORE_WebAPI.Models;
using System.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace CORE_WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Vehicles")]
    [EnableCors("MyPolicy")]
    public class VehiclesController : Controller
    {
        private readonly ProjectCALContext _context;

        public VehiclesController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        //[JsonIgnore]
        public IEnumerable<Vehicle> GetVehicle()
        {
            return _context.Vehicle.Include(vehicle => vehicle.VehicleMake)
                                   .Include(vehicle => vehicle.VehicleType)
                                   .Include(vehicle => vehicle.VehicleStatus);
        }

        // GET: /vehiclegrid
        [HttpGet("/vehiclegrid")]
        public VehicleGrid VehicleGrid()
        {
            VehicleGrid grid = new VehicleGrid();

            grid.totalCount = _context.Vehicle.Include(vehicle => vehicle.VehicleMake)
                                              .Include(vehicle => vehicle.VehicleType)
                                              .Include(vehicle => vehicle.VehicleStatus).Count();


            grid.vehicles = _context.Vehicle.Include(vehicle => vehicle.VehicleMake)
                                            .Include(vehicle => vehicle.VehicleType)
                                            .Include(vehicle => vehicle.VehicleStatus);

            return grid;
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await _context.Vehicle.Include(veh => veh.VehicleMake)
                                                .Include(veh => veh.VehicleType)
                                                .Include(veh => veh.VehicleStatus)
                                                .SingleOrDefaultAsync(m => m.VehicleId == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle([FromRoute] int id, [FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicle.VehicleId)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // POST: api/Vehicles
        [HttpPost]
        public async Task<IActionResult> PostVehicle([FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vehicle.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.VehicleId }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await _context.Vehicle.SingleOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicle.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok(vehicle);
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.VehicleId == id);
        }
    }
}