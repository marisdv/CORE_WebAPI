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
    [Route("api/Shipments")]
    [EnableCors("MyPolicy")]
    public class ShipmentsController : Controller
    {
        private readonly ProjectCALServerContext _context;
        private string baseURL1 = "C:\\img\\signatures\\sender\\";
        private string baseURL2 = "C:\\img\\signatures\\receiver\\";

        public ShipmentsController(ProjectCALServerContext context)
        {
            _context = context;
        }

        // GET: api/Shipments
        [HttpGet]
        public IEnumerable<Shipment> GetShipment()
        {
            return _context.Shipment;
        }

        // GET: /api/shipments/sendersign/4
        [HttpGet("sendersign/{id}")]
        public IActionResult GetSenderSig(int id)
        {
            byte[] imageByte = System.IO.File.ReadAllBytes(baseURL1 + id + ".jpg");
            return File(imageByte, "image/jpeg");
        }
        // GET: /api/shipments/receiversign/4
        [HttpGet("receiversign/{id}")]
        public IActionResult GetReceiverSig(int id)
        {
            byte[] imageByte = System.IO.File.ReadAllBytes(baseURL2 + id + ".jpg");
            return File(imageByte, "image/jpeg");
        }

        // GET: api/Shipments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipment = await _context.Shipment.SingleOrDefaultAsync(m => m.ShipmentId == id);

            if (shipment == null)
            {
                return NotFound();
            }

            return Ok(shipment);
        }

        // GET: api/Shipments/Sender/1
        [HttpGet("sender/{id}")]
        public async Task<IActionResult> GetShipmentBySender([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sender = await _context.Shipment.SingleOrDefaultAsync(m => m.SenderId == id);

            if (sender == null)
            {
                return NotFound();
            }

            return Ok(sender);
        }

        // GET: api/Shipments/Agent/13
        [HttpGet("agent/{id}")]
        public async Task<IActionResult> GetShipmentByAgent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipmentAgent= await _context.Shipment.SingleOrDefaultAsync(m => m.AgentId == id);

            if (shipmentAgent == null)
            {
                return NotFound();
            }

            return Ok(shipmentAgent);
        }

        // PUT: api/Shipments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipment([FromRoute] int id, [FromBody] Shipment shipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shipment.ShipmentId)
            {
                return BadRequest();
            }

            _context.Entry(shipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipmentExists(id))
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

        // POST: api/Shipments
        [HttpPost]
        public async Task<IActionResult> PostShipment([FromBody] Shipment shipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Shipment.Add(shipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShipment", new { id = shipment.ShipmentId }, shipment);
        }

        // DELETE: api/Shipments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipment = await _context.Shipment.SingleOrDefaultAsync(m => m.ShipmentId == id);
            if (shipment == null)
            {
                return NotFound();
            }

            _context.Shipment.Remove(shipment);
            await _context.SaveChangesAsync();

            return Ok(shipment);
        }

        private bool ShipmentExists(int id)
        {
            return _context.Shipment.Any(e => e.ShipmentId == id);
        }
    }
}