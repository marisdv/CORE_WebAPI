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
    [Route("api/Senders")]
    [EnableCors("MyPolicy")]
    public class SendersController : Controller
    {
        private readonly ProjectCALServerContext _context;

        public SendersController(ProjectCALServerContext context)
        {
            _context = context;
        }

        // GET: api/Senders
        [HttpGet]
        public IEnumerable<Sender> GetSender()
        {
            return _context.Sender.Include(login => login.Login);
        }

        // GET: /sendergrid
        [HttpGet("/sendergrid")]
        public SenderGrid SenderGrid()
        {
            SenderGrid grid = new SenderGrid();

            grid.totalCount = _context.Sender.Include(login => login.Login).Count();

            grid.senders = _context.Sender.Include(login => login.Login);

            return grid;
        }

        // GET: api/Senders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSender([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sender = await _context.Sender.Include(login => login.Login)
                                              .SingleOrDefaultAsync(m => m.SenderId == id);

            if (sender == null)
            {
                return NotFound();
            }

            return Ok(sender);
        }

        // GET: api/Senders/phone/012....
        [HttpGet("phone/{id}")]
        public async Task<IActionResult> GetSenderByPhone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sender = await _context.Sender.Include(login => login.Login)
                                                   .SingleOrDefaultAsync(m => m.Login.PhoneNo == id);

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
            Sender updateSender = _context.Sender.FirstOrDefault(s => s.SenderId == id);

            updateSender.UpdateChangedFields(sender);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateSender.SenderId)
            {
                return BadRequest();
            }

            _context.Entry(updateSender).State = EntityState.Modified;

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
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                Sender sender = await _context.Sender.Include(s => s.Shipment).Include(s=>s.BasketLine).Include(l=>l.Login).SingleOrDefaultAsync(s => s.SenderId == id);
                
                Login login = await _context.Login.FirstOrDefaultAsync(l => l.LoginId == sender.LoginId);


                //System.Diagnostics.Debugger.Break();

                if (sender == null)
                {
                    return NotFound("The Sender was not found.");
                }
                
                if (sender.Shipment.Count > 0 || sender.BasketLine.Count > 0)
                {
                    return BadRequest("The selected Sender cannot be deleted because there are items in their basket or they have requested a Shipment before.");
                }
                else
                {
                    _context.Login.Remove(login);

                    await _context.SaveChangesAsync();
                    
                    return Ok(sender);
                }

            }
            catch (Exception ex)
            {
                //System.Diagnostics.Debugger.Break();
                return BadRequest(ex.InnerException.Message);
            }
            
        }

        private bool SenderExists(int id)
        {
            return _context.Sender.Any(e => e.SenderId == id);
        }
    }
}