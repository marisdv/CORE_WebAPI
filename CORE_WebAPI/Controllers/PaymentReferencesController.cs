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
    [Route("api/[controller]")]
    //[ApiController]
    [Produces("application/json")]
    [EnableCors("MyPolicy")]
    public class PaymentReferencesController : ControllerBase
    {
        private readonly ProjectCALServerContext _context;

        public PaymentReferencesController(ProjectCALServerContext context)
        {
            _context = context;
        }

        // GET: api/PaymentReferences
        [HttpGet]
        public IEnumerable<PaymentReference> GetPaymentReference()
        {
            return _context.PaymentReference;
        }

        // GET: api/PaymentReferences/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentReference([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentReference = await _context.PaymentReference.FindAsync(id);

            if (paymentReference == null)
            {
                return NotFound();
            }

            return Ok(paymentReference);
        }

        // PUT: api/PaymentReferences/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentReference([FromRoute] decimal id, [FromBody] PaymentReference paymentReference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentReference.TransactionId)
            {
                return BadRequest();
            }

            _context.Entry(paymentReference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentReferenceExists(id))
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

        // POST: api/PaymentReferences
        [HttpPost]
        public async Task<IActionResult> PostPaymentReference([FromBody] PaymentReference paymentReference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PaymentReference.Add(paymentReference);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PaymentReferenceExists(paymentReference.TransactionId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPaymentReference", new { id = paymentReference.TransactionId }, paymentReference);
        }

        // DELETE: api/PaymentReferences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentReference([FromRoute] decimal id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentReference = await _context.PaymentReference.FindAsync(id);
            if (paymentReference == null)
            {
                return NotFound();
            }

            _context.PaymentReference.Remove(paymentReference);
            await _context.SaveChangesAsync();

            return Ok(paymentReference);
        }

        private bool PaymentReferenceExists(decimal id)
        {
            return _context.PaymentReference.Any(e => e.TransactionId == id);
        }
    }
}