﻿using System;
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
    [Route("api/Penalties")]
    [EnableCors("MyPolicy")]
    public class PenaltiesController : Controller
    {
        private readonly ProjectCALServerContext _context;

        public PenaltiesController(ProjectCALServerContext context)
        {
            _context = context;
        }

        // GET: api/Penalties
        [HttpGet]
        public IEnumerable<Penalty> GetPenalty()
        {
            return _context.Penalty;
        }

        // GET: api/Penalties/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPenalty([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var penalty = await _context.Penalty.SingleOrDefaultAsync(m => m.PentaltyId == id);

            if (penalty == null)
            {
                return NotFound();
            }

            return Ok(penalty);
        }

        // GET: api/penalties/shipment/1
        [HttpGet("shipment/{id}")]
        public async Task<IActionResult> GetPenaltyByShipment([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var penalty = _context.Penalty.Where(m => m.ShipmentId == id);

                if (penalty == null)
                {
                    return NotFound();
                }

                return Ok(penalty);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Penalties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPenalty([FromRoute] int id, [FromBody] Penalty penalty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != penalty.PentaltyId)
            {
                return BadRequest();
            }

            _context.Entry(penalty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PenaltyExists(id))
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


        // POST: api/Penalties/checkpenalty
        [HttpPost("checkpenalty/{id}")]
        public IActionResult CheckPenaltyBySender(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                System.Diagnostics.Debugger.Break();

                var shipments = _context.Shipment.Include(s=>s.Penalty).Where(s => s.SenderId == id).ToList();
                
                foreach (var ship in shipments)
                {
                    if (_context.Penalty.SingleOrDefault(p => p.ShipmentId == ship.ShipmentId && p.DatePaid == null) != null)
                    {
                        return Ok(_context.Penalty.SingleOrDefault(p => p.ShipmentId == ship.ShipmentId));
                    }
                   
                }
                return BadRequest("No Penalty");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Penalties
        [HttpPost]
        public async Task<IActionResult> PostPenalty([FromBody] Penalty penalty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Penalty.Add(penalty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPenalty", new { id = penalty.PentaltyId }, penalty);
        }

        // DELETE: api/Penalties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePenalty([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var penalty = await _context.Penalty.SingleOrDefaultAsync(m => m.PentaltyId == id);
            if (penalty == null)
            {
                return NotFound();
            }

            _context.Penalty.Remove(penalty);
            await _context.SaveChangesAsync();

            return Ok(penalty);
        }

        private bool PenaltyExists(int id)
        {
            return _context.Penalty.Any(e => e.PentaltyId == id);
        }
    }
}