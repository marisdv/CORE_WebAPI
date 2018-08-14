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
    [Route("api/Senders")]
    public class SendersController : Controller
    {
        private readonly ProjectCALContext _context;

        public SendersController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/Senders
        [HttpGet]
        public IEnumerable<Sender> GetSender()
        {
            return _context.Sender;
        }

        // GET: api/Senders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSender([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sender = await _context.Sender.SingleOrDefaultAsync(m => m.SenderId == id);

            if (sender == null)
            {
                return NotFound();
            }

            return Ok(sender);
        }

        // PUT: api/Senders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSender([FromRoute] int id, [FromBody] Sender sender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sender.SenderId)
            {
                return BadRequest();
            }

            _context.Entry(sender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SenderExists(id))
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

        // POST: api/Senders
        [HttpPost]
        public async Task<IActionResult> PostSender([FromBody] Sender sender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Sender.Add(sender);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSender", new { id = sender.SenderId }, sender);
        }

        // DELETE: api/Senders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSender([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sender = await _context.Sender.SingleOrDefaultAsync(m => m.SenderId == id);
            if (sender == null)
            {
                return NotFound();
            }

            _context.Sender.Remove(sender);
            await _context.SaveChangesAsync();

            return Ok(sender);
        }

        private bool SenderExists(int id)
        {
            return _context.Sender.Any(e => e.SenderId == id);
        }
    }
}