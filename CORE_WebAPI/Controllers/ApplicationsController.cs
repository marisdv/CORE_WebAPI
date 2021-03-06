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
    [Route("api/Applications")]
    [EnableCors("MyPolicy")]
    public class ApplicationsController : Controller
    {
        private readonly ProjectCALServerContext _context;

        public ApplicationsController(ProjectCALServerContext context)
        {
            _context = context;
        }

        // GET: api/Applications
        [HttpGet]
        public IEnumerable<Application> GetApplication()
        {
            return _context.Application.Include(application => application.ApplicationStatus)
                                       .Include(agent => agent.Agent)
                                            .ThenInclude(login => login.Login);
        }

        // GET: /applicationgrid
        [HttpGet("/applicationgrid")]
        public ApplicationGrid ApplicationGrid()
        {
            ApplicationGrid grid = new ApplicationGrid();

            grid.totalCount = _context.Application.Include(application => application.ApplicationStatus)
                                                  .Include(agent => agent.Agent)
                                                        .ThenInclude(login => login.Login).Count();

            grid.applications = _context.Application.Include(application => application.ApplicationStatus)
                                                    .Include(agent => agent.Agent)
                                                        .ThenInclude(login => login.Login);

            return grid;
        }

        // GET: api/Applications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplication([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = await _context.Application.Include(appl => appl.ApplicationStatus)
                                                        .Include(agent => agent.Agent)
                                                             .ThenInclude(login => login.Login)
                                                        .SingleOrDefaultAsync(m => m.ApplicationId == id);  

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        // GET: api/Applications/phone/012....
        [HttpGet("phone/{id}")]
        public async Task<IActionResult> GetApplicationByPhone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = await _context.Application.Include(appl => appl.ApplicationStatus)
                                                        .Include(appl => appl.Agent)
                                                            .ThenInclude(agent => agent.Login)
                                                        .SingleOrDefaultAsync(m => m.Agent.Login.PhoneNo == id);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        // GET: api/Applications/agent/13
        [HttpGet("agent/{id}")]
        public async Task<IActionResult> GetApplicationByAgent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = await _context.Application.Include(appl => appl.ApplicationStatus)
                                                        .Include(appl => appl.Agent)
                                                            .ThenInclude(agent => agent.Login)
                                                        .SingleOrDefaultAsync(m => m.AgentId == id);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        // PUT: api/Applications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication([FromRoute] int id, [FromBody] Application application)
        {
            Application updateApplication = _context.Application.FirstOrDefault(a => a.ApplicationId == id);

            updateApplication.UpdateChangedFields(application);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateApplication.ApplicationId)
            {
                return BadRequest();
            }

            _context.Entry(updateApplication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
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

        // POST: api/Applications
        [HttpPost]
        public async Task<IActionResult> PostApplication([FromBody] Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Application.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplication", new { id = application.ApplicationId }, application);
        }

        // DELETE: api/Applications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = await _context.Application.SingleOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }

            _context.Application.Remove(application);
            await _context.SaveChangesAsync();

            return Ok(application);
        }

        private bool ApplicationExists(int id)
        {
            return _context.Application.Any(e => e.ApplicationId == id);
        }
    }
}