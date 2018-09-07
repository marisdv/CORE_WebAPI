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
        private readonly ProjectCALContext _context;

        public EmployeesController(ProjectCALContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> GetEmployee()
        {
            return _context.Employee.Include(accessRole => accessRole.AccessRole)
                                    .Include(login => login.Login);
        }

        // GET: /employeegrid
        [HttpGet("/employeegrid")]
        public EmployeeGrid EmployeeGrid()
        {
            EmployeeGrid grid = new EmployeeGrid();

            grid.totalCount = _context.Employee.Include(accessRole => accessRole.AccessRole)
                                                .Include(login => login.Login).Count();

            grid.employees = _context.Employee.Include(accessRole => accessRole.AccessRole)
                                              .Include(login => login.Login);
            return grid;
        }

        /*
        // GET: api/Employees/BasicDetails
        // *** find a way to route this method
        [HttpGet]
        public IEnumerable<string> GetEmployeeBD()
        {
            List<string> employeeDetails = new List<string>();

            _context.Employee.ForEachAsync(x => employeeDetails.Add(x.GetBasicDetails()));

            return employeeDetails;
        }*/

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employee/*.Include(accessRole => accessRole.AccessRole)*/
                                                 .Include(login => login.Login)
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

            var application = await _context.Employee.Include(login => login.Login)
                                                   .SingleOrDefaultAsync(m => m.Login.PhoneNo == id);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            Employee updateEmployee = _context.Employee.FirstOrDefault(e => e.EmployeeId == id);

            updateEmployee.UpdateChangedFields(employee);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateEmployee.EmployeeId)
            {
                return BadRequest();
            }

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

            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employee.SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }
    }
}