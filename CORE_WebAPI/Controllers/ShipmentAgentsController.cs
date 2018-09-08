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
    [Route("api/ShipmentAgents")]
    [EnableCors("MyPolicy")]
    public class ShipmentAgentsController : Controller
    {
        private readonly ProjectCALContext _context;

        public ShipmentAgentsController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/ShipmentAgents
        [HttpGet]
        public IEnumerable<ShipmentAgent> GetShipmentAgent()
        {
            return _context.ShipmentAgent/*.Include(image => image.AgentImage)
                                         .Include(loc => loc.ShipmentAgentLocation)
                                         .Include(licence => licence.LicenceImage)*/
                                         .Include(login => login.Login)
                                         /*.Include(city => city.City)*/;
        }

        // GET: /shipmentagentgrid
        [HttpGet("/shipmentagentgrid")]
        public ShipmentAgentGrid ShipmentAgentGrid()
        {
            ShipmentAgentGrid grid = new ShipmentAgentGrid();

            grid.totalCount = _context.ShipmentAgent/*.Include(image => image.AgentImage)
                                                    .Include(loc => loc.ShipmentAgentLocation)
                                                    .Include(licence => licence.LicenceImage)*/
                                                    .Include(login => login.Login)
                                                    /*.Include(city => city.City)*/
                                                     .Count();

            grid.shipmentAgents = _context.ShipmentAgent/*.Include(image => image.AgentImage)
                                                        .Include(loc => loc.ShipmentAgentLocation)
                                                        .Include(licence => licence.LicenceImage)*/
                                                        .Include(login => login.Login)
                                                        /*.Include(city => city.City)*/;

            return grid;
        }

        // GET: api/ShipmentAgents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipmentAgent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipmentAgent = await _context.ShipmentAgent.Include(image => image.AgentImage)
                                                            .Include(loc => loc.ShipmentAgentLocation)
                                                            .Include(licence => licence.LicenceImage)
                                                            .Include(login => login.Login)
                                                            .Include(city => city.City)
                                                            .SingleOrDefaultAsync(m => m.AgentId == id);

            if (shipmentAgent == null)
            {
                return NotFound();
            }

            return Ok(shipmentAgent);
        }

        // GET: api/ShipmentAgents/phone/082...
        [HttpGet("phone/{id}")]
        public async Task<IActionResult> GetShipmentAgentByPhone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipmentAgent = await _context.ShipmentAgent
                                                .Include(login => login.Login)
                                                .SingleOrDefaultAsync(m => m.Login.PhoneNo == id);


            if (shipmentAgent == null)
            {
                return NotFound();
            }

            return Ok(shipmentAgent);
        }

        // PUT: api/ShipmentAgents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipmentAgent([FromRoute] int id, [FromBody] ShipmentAgent shipmentAgent)
        {
            ShipmentAgent updateAgent = _context.ShipmentAgent.FirstOrDefault(a => a.AgentId == id);

            updateAgent.UpdateChangedFields(shipmentAgent);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateAgent.AgentId)
            {
                return BadRequest();
            }

            _context.Entry(updateAgent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipmentAgentExists(id))
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

        // POST: api/ShipmentAgents
        [HttpPost]
        public async Task<IActionResult> PostShipmentAgent([FromBody] ShipmentAgent shipmentAgent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ShipmentAgent.Add(shipmentAgent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShipmentAgent", new { id = shipmentAgent.AgentId }, shipmentAgent);
        }

        // DELETE: api/ShipmentAgents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipmentAgent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipmentAgent = await _context.ShipmentAgent.SingleOrDefaultAsync(m => m.AgentId == id);
            if (shipmentAgent == null)
            {
                return NotFound();
            }

            _context.ShipmentAgent.Remove(shipmentAgent);
            await _context.SaveChangesAsync();

            return Ok(shipmentAgent);
        }

        private bool ShipmentAgentExists(int id)
        {
            return _context.ShipmentAgent.Any(e => e.AgentId == id);
        }
    }
}