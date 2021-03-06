﻿using System;
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
    public class ShipmentAgentNotificationsController : ControllerBase
    {
        private readonly ProjectCALServerContext _context;

        public ShipmentAgentNotificationsController(ProjectCALServerContext context)
        {
            _context = context;
        }

        // GET: api/ShipmentAgentNotifications
        [HttpGet]
        public IEnumerable<ShipmentAgentNotification> GetShipmentAgentNotification()
        {
            return _context.ShipmentAgentNotification;
        }

        // GET: api/ShipmentAgentNotifications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipmentAgentNotification([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipmentAgentNotification = await _context.ShipmentAgentNotification.FindAsync(id);

            if (shipmentAgentNotification == null)
            {
                return NotFound();
            }

            return Ok(shipmentAgentNotification);
        }

        // GET: api/ShipmentAgentNotifications/notification/5
        [HttpGet("notification/{id}")]
        public async Task<IActionResult> GetNotificationByNotification([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notification = await _context.ShipmentAgentNotification.SingleOrDefaultAsync(m => m.NotificationId == id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        // GET: api/ShipmentAgentNotifications/agent/17
        [HttpGet("agent/{id}")]
        public async Task<IActionResult> GetNotificationByAgent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agent = _context.ShipmentAgentNotification.Where(m => m.AgentId == id);

            if (agent == null)
            {
                return NotFound();
            }

            return Ok(agent);
        }

        // PUT: api/ShipmentAgentNotifications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipmentAgentNotification([FromRoute] int id, [FromBody] ShipmentAgentNotification shipmentAgentNotification)
        {
            ShipmentAgentNotification updateNotif = _context.ShipmentAgentNotification.FirstOrDefault(n => n.NotificationId == id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateNotif.NotificationId)
            {
                return BadRequest();
            }

            updateNotif.UpdateChangedFields(shipmentAgentNotification);

            _context.Entry(updateNotif).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipmentAgentNotificationExists(id))
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

        // POST: api/ShipmentAgentNotifications
        [HttpPost]
        public async Task<IActionResult> PostShipmentAgentNotification([FromBody] ShipmentAgentNotification shipmentAgentNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ShipmentAgentNotification.Add(shipmentAgentNotification);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShipmentAgentNotificationExists(shipmentAgentNotification.NotificationId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetShipmentAgentNotification", new { id = shipmentAgentNotification.NotificationId }, shipmentAgentNotification);
        }

        // DELETE: api/ShipmentAgentNotifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipmentAgentNotification([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipmentAgentNotification = await _context.ShipmentAgentNotification.FindAsync(id);
            if (shipmentAgentNotification == null)
            {
                return NotFound();
            }

            _context.ShipmentAgentNotification.Remove(shipmentAgentNotification);
            await _context.SaveChangesAsync();

            return Ok(shipmentAgentNotification);
        }

        private bool ShipmentAgentNotificationExists(int id)
        {
            return _context.ShipmentAgentNotification.Any(e => e.NotificationId == id);
        }
    }
}