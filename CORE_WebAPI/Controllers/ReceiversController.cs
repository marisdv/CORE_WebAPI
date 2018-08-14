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
    [Produces("application/json")]
    [Route("api/Receivers")]
    public class ReceiversController : Controller
    {
        private readonly ProjectCALContext _context;

        public ReceiversController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/Receivers
        [HttpGet]
        public IEnumerable<Receiver> GetReceiver()
        {
            return _context.Receiver;
        }

        // GET: api/Receivers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReceiver([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receiver = await _context.Receiver.SingleOrDefaultAsync(m => m.ReceiverId == id);

            if (receiver == null)
            {
                return NotFound();
            }

            return Ok(receiver);
        }

        // PUT: api/Receivers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiver([FromRoute] int id, [FromBody] Receiver receiver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != receiver.ReceiverId)
            {
                return BadRequest();
            }

            _context.Entry(receiver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiverExists(id))
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

        // POST: api/Receivers
        [HttpPost]
        public async Task<IActionResult> PostReceiver([FromBody] Receiver receiver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Receiver.Add(receiver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceiver", new { id = receiver.ReceiverId }, receiver);
        }

        // DELETE: api/Receivers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceiver([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receiver = await _context.Receiver.SingleOrDefaultAsync(m => m.ReceiverId == id);
            if (receiver == null)
            {
                return NotFound();
            }

            _context.Receiver.Remove(receiver);
            await _context.SaveChangesAsync();

            return Ok(receiver);
        }

        private bool ReceiverExists(int id)
        {
            return _context.Receiver.Any(e => e.ReceiverId == id);
        }
    }
}