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
    [Route("api/Logins")]
    [EnableCors("MyPolicy")]
    public class LoginsController : Controller
    {
        private readonly ProjectCALServerContext _context;

        public LoginsController(ProjectCALServerContext context)
        {
            _context = context;
        }

        // GET: api/Logins
        [HttpGet]
        public IEnumerable<Login> GetLogin()
        {
            return _context.Login/*.Include(login => login.UserType)*/;
        }

        // GET: api/Logins/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLogin([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var login = await _context.Login/*.Include(login => login.UserType)*/
                                            .SingleOrDefaultAsync(m => m.LoginId == id);

            if (login == null)
            {
                return NotFound();
            }

            return Ok(login);
        }

        // GET: api/Logins/phone/082...
        [HttpGet("phone/{id}")]
        public async Task<IActionResult> GetLoginByPhone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var login = await _context.Login/*.Include(login => login.UserType)*/
                                            .SingleOrDefaultAsync(m => m.PhoneNo == id);

            if (login == null)
            {
                return NotFound();
            }

            return Ok(login);
        }

        // POST: api/Logins/verify/082...
        [HttpPost("verify/{id}")]
        public async Task<IActionResult> VerifyPassword([FromRoute] string id,[FromBody] Login login)
        {
            bool valid = false;
            try
            {
                
                var log = await _context.Login/*.Include(login => login.UserType)*/
                                                            .SingleOrDefaultAsync(m => m.PhoneNo == id);

                if (log == null)
                {
                    return NotFound("No Login linked to this cell number.");
                }
                if (log.verifyPassword(login.Password))
                {
                    valid = true;
                    return Ok(valid);
                }
                else return BadRequest(valid);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        // PUT: api/Logins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogin([FromRoute] int id, [FromBody] Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id != login.LoginId)
                {
                    return BadRequest();
                }
                //System.Diagnostics.Debugger.Break();

                Login updateLogin = _context.Login.FirstOrDefault(l => l.LoginId == id);

                //System.Diagnostics.Debugger.Break();

                updateLogin.UpdateChangedFields(login);

                updateLogin.hashPassword();


                _context.Entry(updateLogin).State = EntityState.Modified; //breaks here
                //System.Diagnostics.Debugger.Break();
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(id))
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Logins
        [HttpPost]
        public async Task<IActionResult> PostLogin([FromBody] Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("No idea what went wrong"/*ModelState*/);
                }

                login.hashPassword();

                _context.Login.Add(login);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetLogin", new { id = login.LoginId }, login);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        // DELETE: api/Logins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogin([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var login = await _context.Login.SingleOrDefaultAsync(m => m.LoginId == id);
            if (login == null)
            {
                return NotFound();
            }

            _context.Login.Remove(login);
            await _context.SaveChangesAsync();

            return Ok(login);
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.LoginId == id);
        }
    }
}