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
    [Route("api/AccessRoles")]
    [EnableCors("MyPolicy")]
    public class AccessRolesController : Controller
    {
        private readonly ProjectCALContext _context;

        public AccessRolesController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/AccessRoles
        [HttpGet]
        public IEnumerable<AccessRole> GetAccessRole()
        {
            return _context.AccessRole/*.Include(AccessRoleArea => AccessRoleArea.AccessRoleArea)
                                        .Include(AccessArea => AccessArea.AccessArea)*/;
        }

        //// GET: api/AccessRoles
        //[HttpGet]
        //public IEnumerable<AccessRole> GetAccessRole()
        //{
        //    return _context.AccessRole.Include(AccessRoleArea => AccessRoleArea.AccessRoleArea)
        //                              .ThenInclude(AccessArea => AccessArea.AccessArea);
        //}

        // GET: /accessrolegrid
        [HttpGet("/accessrolegrid")]
        public AccessRoleGrid AccessRoleGrid()
        {
            AccessRoleGrid grid = new AccessRoleGrid();

            grid.totalCount = _context.AccessRole/*.Include(AccessRoleArea => AccessRoleArea.AccessRoleArea)
                                      .Include(AccessArea => AccessArea.AccessArea)*/.Count();

            grid.accessRoles = _context.AccessRole/*.Include(AccessRoleArea => AccessRoleArea.AccessRoleArea)
                                      .Include(AccessArea => AccessArea.AccessArea)*/;

            return grid;
        }

        // GET: api/AccessRoles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccessRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accessRole = await _context.AccessRole/*.Include(AccessRoleArea => AccessRoleArea.AccessRoleArea)
                                      .Include(AccessArea => AccessArea.AccessArea)*/
                                                      .SingleOrDefaultAsync(m => m.AccessRoleId == id);

            if (accessRole == null)
            {
                return NotFound();
            }

            return Ok(accessRole);
        }

        // PUT: api/AccessRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessRole([FromRoute] int id, [FromBody] AccessRole accessRole)
        {
            AccessRole updateAccessRole = _context.AccessRole.FirstOrDefault(a => a.AccessRoleId == id);

            updateAccessRole.UpdateChangedFields(accessRole);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateAccessRole.AccessRoleId)
            {
                return BadRequest();
            }

            _context.Entry(updateAccessRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessRoleExists(id))
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

        // POST: api/AccessRoles
        [HttpPost]
        public async Task<IActionResult> PostAccessRole([FromBody] AccessRole accessRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<int> areas = new List<int>();
            foreach (var area in accessRole.AccessRoleArea)
            {
                areas.Add(area.AccessAreaId);
            }

            AccessRole role = new AccessRole();

            role = accessRole;
            role.AccessRoleArea.Clear();
            if (_context.AccessRole.FirstOrDefault(dbRole => dbRole.AccessRoleName == role.AccessRoleName) == null)
            {
                _context.AccessRole.Add(role);
                await _context.SaveChangesAsync();

                role = _context.AccessRole.Last();

                foreach (var id in areas)
                {
                    AccessRoleArea addRoleArea = new AccessRoleArea();
                    addRoleArea.AccessAreaId = id;
                    addRoleArea.AccessRoleId = role.AccessRoleId;

                    role.AccessRoleArea.Add(addRoleArea);
                }

                _context.Entry(role).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetAccessRole", new { id = accessRole.AccessRoleId }, accessRole);
            }
            else return BadRequest("A role with this Name already exists.");

        }

        // DELETE: api/AccessRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accessRole = await _context.AccessRole.SingleOrDefaultAsync(m => m.AccessRoleId == id);
            if (accessRole == null)
            {
                return NotFound();
            }

            _context.AccessRole.Remove(accessRole);
            await _context.SaveChangesAsync();

            return Ok(accessRole);
        }

        private bool AccessRoleExists(int id)
        {
            return _context.AccessRole.Any(e => e.AccessRoleId == id);
        }
    }
}