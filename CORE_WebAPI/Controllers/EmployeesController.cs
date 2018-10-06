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
    [Route("api/Employees")]
    [EnableCors("MyPolicy")]
    public class EmployeesController : Controller
    {
        private readonly ProjectCALServerContext _context;
        private string baseURL = "C:\\img\\employees\\";

        public EmployeesController(ProjectCALServerContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> GetEmployee()
        {
            return _context.Employee.Include(accessRole => accessRole.AccessRole);
        }

        // GET: /api/employees/image/13
        [HttpGet("image/{id}")]
        public IActionResult GetEmployeeImage(int id)
        {
            byte[] imageByte = System.IO.File.ReadAllBytes(baseURL + id + ".jpg");
            return File(imageByte, "image/jpeg");
        }

        // GET: /employeegrid
        [HttpGet("/employeegrid")]
        public EmployeeGrid EmployeeGrid()
        {
            EmployeeGrid grid = new EmployeeGrid();

            grid.totalCount = _context.Employee.Include(accessRole => accessRole.AccessRole).Count();

            grid.employees = _context.Employee.Include(accessRole => accessRole.AccessRole);
            return grid;
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employee.Include(accessRole => accessRole.AccessRole)
                                                 .SingleOrDefaultAsync(m => m.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // GET: api/Employees/phone/012....
        [HttpGet("phone/{id}")]
        public async Task<IActionResult> GetEmployeeByPhone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = await _context.Employee
                                                   .SingleOrDefaultAsync(m => m.EmployeePhone == id);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        //Verify Employee Password
        [HttpPost("verify/{id}")]
        public async Task<IActionResult> VerifyPassword([FromRoute] string id, [FromBody] Employee employee)
        {
            bool valid = false;
            try
            {

                var log = await _context.Employee
                                            .SingleOrDefaultAsync(m => m.EmployeePhone == id);

                if (log == null)
                {
                    return NotFound("No Login linked to this cell number.");
                }
                if (log.verifyPassword(employee.EmployeePassword))
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

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            System.Diagnostics.Debugger.Break();
            Employee updateEmployee = _context.Employee.FirstOrDefault(e => e.EmployeeId == id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateEmployee.EmployeeId)
            {
                return BadRequest();
            }

            updateEmployee.UpdateChangedFields(employee);

            updateEmployee.hashPassword();


            _context.Entry(updateEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            employee.hashPassword();
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Employee employee = await _context.Employee.Include(m => m.Application).SingleOrDefaultAsync(m => m.EmployeeId == id);

                if (employee == null)
                {
                    return NotFound();
                }

                if (employee.Application.Count > 0)// || ||)
                {
                    return BadRequest("The selected Employee cannot be deleted because they have existing reviewed applications.");
                }
                else
                {
                    _context.Employee.Remove(employee);
                    await _context.SaveChangesAsync();

                    return Ok(employee);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }
    }
}