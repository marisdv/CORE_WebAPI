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
        private readonly ProjectCALServerContext _context;

        public VehicleTypesController(ProjectCALServerContext context)
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

        // GET: api/VehicleTypes/descr/{descr}....
        [HttpGet("descr/{id}")]
        public async Task<IActionResult> GetTypeByDescr([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var type = await _context.VehicleType.SingleOrDefaultAsync(m => m.VehicleTypeDescr == id);

            if (type == null)
            {
                return NotFound();
            }

            return Ok(type);
        }

        // PUT: api/VehicleTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleType([FromRoute] int id, [FromBody] VehicleType vehicleType)
        {
            VehicleType updateVehicleType = _context.VehicleType.FirstOrDefault(v => v.VehicleTypeId == id);

            updateVehicleType.UpdateChangedFields(vehicleType);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateVehicleType.VehicleTypeId)
            {
                return BadRequest();
            }

            _context.Entry(updateVehicleType).State = EntityState.Modified;

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

        public struct vehiclePackages
        {
            public string vehicleTypeDescription { get; set; }
            public List<packageType> packageTypes { get; set; }
        }

        public struct packageType
        {
            public int packageTypeId { get; set; }
            public int quantity { get; set; }
        }

        // POST: api/VehicleTypes
        [HttpPost]
        public async Task<IActionResult> PostVehicleType([FromBody] vehiclePackages vehicleType)
        {
             System.Diagnostics.Debugger.Break();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<packageType> types = new List<packageType>();
            //doesn't run through this loop
            VehicleType vehicle = new VehicleType();

            types = vehicleType.packageTypes;

            vehicle.VehicleTypeDescr = vehicleType.vehicleTypeDescription;

            if (_context.VehicleType.FirstOrDefault(dbVeh => dbVeh.VehicleTypeDescr == vehicle.VehicleTypeDescr) == null)
            {
                _context.VehicleType.Add(vehicle);
                await _context.SaveChangesAsync();

                vehicle = _context.VehicleType.Last();

                foreach (var type in types)
                {
                    VehiclePacakageLine addPackLine = new VehiclePacakageLine();
                    addPackLine.PackageTypeId = type.packageTypeId;
                    addPackLine.VehicleTypeId = vehicle.VehicleTypeId;
                    addPackLine.Quantity = type.quantity;

                    vehicle.VehiclePacakageLine.Add(addPackLine);
                }

                //_context.VehicleType.Add(vehicleType);
                _context.Entry(vehicle).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                System.Diagnostics.Debugger.Break();
                return CreatedAtAction("GetVehicleType", new { id = vehicle.VehicleTypeId }, vehicleType);
            }

            //System.Diagnostics.Debugger.Break();
            else  return BadRequest("A vehicle type with this name already exists");
        }

        // DELETE: api/VehicleTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleType([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //should vehicle be included?
                VehicleType vehicleType = await _context.VehicleType.Include(v => v.VehiclePacakageLine).Include(v => v.Vehicle).SingleOrDefaultAsync(v => v.VehicleTypeId == id);

                if (vehicleType == null)
                {
                    return NotFound("The Vehicle Type was not found.");
                }

                System.Diagnostics.Debugger.Break();

                if (vehicleType.Vehicle.Count > 0)
                {
                    return BadRequest("The selected Vehicle Type cannot be deleted because it is assigned to a Vehicle.");
                }
                else
                {
                    
                    _context.VehicleType.Remove(vehicleType);

                    //also delete vehicle package lines where this vehicle type is used

                    await _context.SaveChangesAsync();

                    return Ok(vehicleType);
                    
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
                return BadRequest(ex.Message);
            }
            
        }

        private bool VehicleTypeExists(int id)
        {
            return _context.VehicleType.Any(e => e.VehicleTypeId == id);
        }
    }
}